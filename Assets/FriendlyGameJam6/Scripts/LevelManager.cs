﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerStatus
{
    public int Lives = 20;

    public int Money = 0;

    public int CitizenMax = 10;

    public List<CitizenWeapon> Weapons;

    public List<GameObject> Citizens;
}

[Serializable]
public class GameConstants
{
    public float CitizenSellRatio = 0.5f;

    public float GrenadeRadius = 2f;

    public bool RemoveOnEquip = true;

    public AudioClip PunchSound;

    public float DisplayMessageDefaultDuration = 2;

    public float DisplayErrorDefaultDuration = 1;

    public float DisplayMessageDefaultExit = 0.25f;

    public float DisplayMessageDefaultEnter = 0.25f;
}

public class LevelManager : MonoBehaviour
{
    // s_Instance is used to cache the instance found in the scene so we 
    // don't have to look it up every time.
    private static LevelManager s_instance = null;

    public Transform EnemyDestination;

    public Transform CitizenSpawnPoint;

    public List<Transform> AlienSpawnPoints;

    public List<Wave> Waves = new List<Wave>();

    [SerializeField]
    public PlayerStatus Player = new PlayerStatus();

    [SerializeField]
    public GameConstants Constants = new GameConstants();

    [HideInInspector]
    public PlayerInput PlayerInput;

    public List<CitizenType> AvailableCitizens = new List<CitizenType>();

    [HideInInspector]
    public Commands Command;

    public List<Alien> Aliens = new List<Alien>();

    public string NextScene;

    private int curWaveIndex = 0;
    private bool playNextWave = false;
    private bool waitingForNextWave = false;

    // The simple getter method (usage: GB_Environment.Instance)
    public static LevelManager Instance
    {
        get
        {
            return s_instance;
        }
    }

    public bool quitting = false;

    private void Awake()
    {
        // Assign this specific instance to be the global singleton
        // If another exists we will not assign it and display an error
        if (s_instance == null)
        {
            s_instance = this;
            PlayerInput = GetComponent<PlayerInput>();
            Command = GetComponent<Commands>();
            if (Waves.Count == 0)
            {
                Debug.LogWarning("There are no waves set on this map!");
            }
        }
        else
        {
            Debug.LogWarning("Another instance of GB_Environment exists.");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        UpdateEnemySpawns();
        if (Player.Lives <= 0 && !quitting)
        {
            StatusManager.DisplayStatus("Game Over");
            StartCoroutine(QuitGame());
        }
        if (Aliens.Count == 0 && curWaveIndex == Waves.Count && !quitting)
        {
            if (NextScene == null)
            {
                StatusManager.DisplayStatus("Game Complete");
            }
            StatusManager.DisplayStatus("Level Complete");
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        quitting = true;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(NextScene);
    }

    private IEnumerator QuitGame()
    {
        quitting = true;
        yield return new WaitForSeconds(3);
        StatusManager.DisplayStatus("Restarting Now...");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateEnemySpawns()
    {
        if (playNextWave)
        {
            if (curWaveIndex > 0)
            {
                // Check for a reward and give it to the player
                if (Waves[curWaveIndex - 1].Reward != null) {
                    Command.AddWeapon(Waves[curWaveIndex - 1].Reward);
                    StartCoroutine(DelayMessageForGun(Waves[curWaveIndex - 1].Reward));
                }
            }

            StatusManager.DisplayStatus(string.Format("Wave {0}", curWaveIndex + 1));
            Command.BeginSpawning(Waves[curWaveIndex]);
            curWaveIndex++;
            playNextWave = false;
        }

        if (curWaveIndex < Waves.Count)
        {
            if (float.IsInfinity(Waves[curWaveIndex].WaitForSeconds))
            {
                if (Aliens.Count == 0)
                {
                    playNextWave = true;
                }
            }
            else
            {
                if (!waitingForNextWave)
                {
                    StartCoroutine(WaitForNextWave(Waves[curWaveIndex].WaitForSeconds));
                }
            }
        }
    }

    private IEnumerator DelayMessageForGun(CitizenWeapon weapon)
    {
        yield return new WaitForSeconds(2.5f);
        StatusManager.DisplayError(string.Format("Recieved {0}", weapon.Name));
    }

    private IEnumerator WaitForNextWave(float duration)
    {
        waitingForNextWave = true;
        yield return new WaitForSeconds(duration);
        playNextWave = true;
        waitingForNextWave = false;
    }

    // Ensure that the instance is destroyed when the game is stopped in 
    // the editor.
    void OnApplicationQuit()
    {
        s_instance = null;
    }
}
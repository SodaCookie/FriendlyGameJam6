using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerStatus
{
    public int Lives = 20;

    public int Money = 0;

    public List<CitizenWeapon> Weapons;
}

public class LevelManager : MonoBehaviour
{
    // s_Instance is used to cache the instance found in the scene so we 
    // don't have to look it up every time.
    private static LevelManager s_instance = null;

    public Transform Target;

    public List<Wave> Waves = new List<Wave>();

    [SerializeField]
    public PlayerStatus Player = new PlayerStatus();

    public List<CitizenType> Citizens = new List<CitizenType>();

    [HideInInspector]
    public Commands Command = new Commands();

    [HideInInspector]
    public List<Alien> Aliens = new List<Alien>();

    // The simple getter method (usage: GB_Environment.Instance)
    public static LevelManager Instance
    {
        get
        {
            return s_instance;
        }
    }

    private void Awake()
    {
        // Assign this specific instance to be the global singleton
        // If another exists we will not assign it and display an error
        if (s_instance == null)
        {
            s_instance = this;
        }
        else
        {
            Debug.LogWarning("Another instance of GB_Environment exists.");
            Destroy(gameObject);
        }
    }

    //private void Update()
    //{
    //    UpdateEnemySpawns();
    //}

    //private void UpdateEnemySpawns()
    //{
    //    throw new NotImplementedException();
    //}

    // Ensure that the instance is destroyed when the game is stopped in 
    // the editor.
    void OnApplicationQuit()
    {
        s_instance = null;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    // s_Instance is used to cache the instance found in the scene so we 
    // don't have to look it up every time.
    private static StatusManager s_instance = null;

    [SerializeField]
    private GameObject ErrorContainer;

    [SerializeField]
    private GameObject StatusContainer;

    [SerializeField]
    private TMPro.TextMeshProUGUI ErrorText;

    [SerializeField]
    private TMPro.TextMeshProUGUI StatusText;

    private float duration = 0;
    private bool textChangeable = false;

    // The simple getter method (usage: GB_Environment.Instance)
    public static StatusManager Instance
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

    public static void DisplayError(string msg)
    {
        Instance.duration = LevelManager.Instance.Constants.DisplayErrorDefaultDuration;
        if (Instance.textChangeable && Instance.ErrorContainer.activeInHierarchy)
        {
            Instance.ErrorText.text = msg;
            return;
        }

        // Change the text
        Instance.StopAllDisplayedMessage();
        Instance.ErrorText.text = msg;
        Instance.StartCoroutine(DisplayMessage(
            LevelManager.Instance.Constants.DisplayMessageDefaultEnter,
            LevelManager.Instance.Constants.DisplayMessageDefaultExit,
            Instance.ErrorContainer));
    }

    public static void DisplayStatus(string msg)
    {
        Instance.duration = LevelManager.Instance.Constants.DisplayMessageDefaultDuration;
        if (Instance.textChangeable && Instance.StatusContainer.activeInHierarchy)
        {
            Instance.StatusText.text = msg;
            return;
        }

        // Change the text
        Instance.StopAllDisplayedMessage();
        Instance.StatusText.text = msg;
        Instance.StartCoroutine(DisplayMessage(
            LevelManager.Instance.Constants.DisplayMessageDefaultEnter,
            LevelManager.Instance.Constants.DisplayMessageDefaultExit,
            Instance.StatusContainer));
    }

    private void StopAllDisplayedMessage()
    {
        Instance.StopAllCoroutines();
        Instance.StatusContainer.SetActive(false);
        Instance.ErrorContainer.SetActive(false);
        textChangeable = false;
    }

    // while displaying the message, messages of the same type will simply change the text
    // not stop the actual coroutine
    private static IEnumerator DisplayMessage(float enterDuration, float exitDuration, GameObject target)
    {
        Instance.textChangeable = true;
        {
            target.SetActive(true);
            float startTime = Time.time;
            target.transform.localScale = new Vector3(1, 0, 1);
            while (Time.time - startTime < enterDuration)
            {
                target.transform.localScale = new Vector3(1, (Time.time - startTime) / enterDuration, 1);
                yield return null;
            }
            target.transform.localScale = new Vector3(1, 1, 1);
        }

        while (Instance.duration > 0)
        {
            Instance.duration -= Time.deltaTime;
            yield return null;
        }
        Instance.textChangeable = false;

        {
            float startTime = Time.time;
            while (Time.time - startTime < exitDuration)
            {
                target.transform.localScale = new Vector3(1, 1 - (Time.time - startTime) / enterDuration, 1);
                yield return null;
            }
            target.transform.localScale = new Vector3(1, 0, 1);
            target.SetActive(false);
        }
    }
}

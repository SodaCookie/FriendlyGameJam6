using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplayMessages());
    }

    // Update is called once per frame
    IEnumerator DisplayMessages()
    {
        StatusManager.DisplayError("This is a test error");
        yield return new WaitForSeconds(3);
        StatusManager.DisplayStatus("This is a test status update\n with a multiline");
        yield return new WaitForSeconds(3);
        StatusManager.DisplayStatus("Here's another message");
        yield return new WaitForSeconds(1);
        StatusManager.DisplayStatus("Woah dude sudden switch");
        yield return new WaitForSeconds(1);
        StatusManager.DisplayError("Woah dude error switch");
    }
}

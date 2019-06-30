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
        StatusManager.DisplayError("Tutorial");
        yield return new WaitForSeconds(2.5f);
        StatusManager.DisplayStatus("Welcome to Citizen Defense.");
        yield return new WaitForSeconds(2.5f);
        StatusManager.DisplayStatus("Buy citizens by purchasing them\nalong the citizen bar.");
        yield return new WaitForSeconds(2.5f);
        StatusManager.DisplayStatus("Move citizens to platforms to begin attacking\n aliens by selecting a citizen to move\nand left clicking an empty platform.");
        yield return new WaitForSeconds(2.5f);
        StatusManager.DisplayStatus("Right click citizens to sell them.\n You can also sell guns by\nhitting the red button next to the gun name.");
        yield return new WaitForSeconds(2.5f);
        StatusManager.DisplayError("Have fun and good luck!");
    }
}

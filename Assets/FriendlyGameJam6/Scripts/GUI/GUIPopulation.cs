using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIPopulation : MonoBehaviour
{
    TMPro.TextMeshProUGUI Text;

    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = string.Format("{0}/{1}", LevelManager.Instance.Player.Citizens.Count, LevelManager.Instance.Player.CitizenMax);
    }
}

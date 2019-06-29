using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUILives : MonoBehaviour
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
        Text.text = LevelManager.Instance.Player.Lives.ToString();
    }
}

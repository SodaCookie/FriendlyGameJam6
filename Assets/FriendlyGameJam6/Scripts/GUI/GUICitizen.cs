using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUICitizen : MonoBehaviour
{
    public TMPro.TextMeshProUGUI NameText;

    public TMPro.TextMeshProUGUI CostText;

    private Citizen citizen;

    private GameObject citizenPrefab;

    public void Initialize(GameObject cPrefab)
    {
        citizenPrefab = cPrefab;
        citizen = citizenPrefab.GetComponent<Citizen>();
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(CreateCitizen);
    }

    public void CreateCitizen()
    {
        if (LevelManager.Instance.Player.Money >= citizen.CitizenRole.Cost && LevelManager.Instance.Player.Citizens.Count < LevelManager.Instance.Player.CitizenMax)
        {
            LevelManager.Instance.Command.CreateCitizen(citizenPrefab);
        }
        else
        {
            StatusManager.DisplayError("You do not have enough funds.");
        }
    }
}

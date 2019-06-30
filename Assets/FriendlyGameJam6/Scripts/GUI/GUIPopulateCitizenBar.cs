using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIPopulateCitizenBar : MonoBehaviour
{
    public GameObject CitizenCardPrefab;

    void Start()
    {
        int count = LevelManager.Instance.AvailableCitizens.Count - 1;
        foreach (CitizenType type in LevelManager.Instance.AvailableCitizens)
        {
            var child = Instantiate(CitizenCardPrefab, transform);
            GUICitizen guiElements = child.GetComponent<GUICitizen>();
            guiElements.Initialize(type.PrefabGameObject);
            guiElements.NameText.text = type.Name;
            guiElements.CostText.text = string.Format("${0}", type.Cost);

            // Positioning
            RectTransform rTransform = child.GetComponent<RectTransform>();
            rTransform.anchoredPosition = new Vector2(count * 80, 0);

            count--;
        }
    }
}

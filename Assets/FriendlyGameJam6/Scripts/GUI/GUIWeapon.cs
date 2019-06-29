using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIWeapon : MonoBehaviour
{
    public CitizenWeapon WeaponToUse;

    private void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(AssignWeapon);
    }

    public void AssignWeapon()
    {
        if (NumberOfWeaponType(WeaponToUse.Name) > 0)
        {
            LevelManager.Instance.PlayerInput.SetSelection(WeaponToUse, SelectionType.Weapon);
        }
    }

    public static int NumberOfWeaponType(string name)
    {
        int count = 0;
        for (int i = 0; i < LevelManager.Instance.Player.Weapons.Count; i++)
        {
            if (LevelManager.Instance.Player.Weapons[i].Name == name)
            {
                count++;
            }
        }
        return count;
    }
}

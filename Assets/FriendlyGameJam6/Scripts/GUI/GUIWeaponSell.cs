using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIWeaponSell : MonoBehaviour
{
    public CitizenWeapon WeaponToUse;

    private void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(SellWeapon);
    }

    public void SellWeapon()
    {
        if (GUIWeapon.NumberOfWeaponType(WeaponToUse.Name) > 0)
        {
            LevelManager.Instance.Command.SellWeapon(WeaponToUse.Name);
        }
    }
}

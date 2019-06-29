using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIWeaponCount : MonoBehaviour
{
    public CitizenWeapon weapon;

    TMPro.TextMeshProUGUI Text;

    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = string.Format("{0} x{1}", weapon.Name, GUIWeapon.NumberOfWeaponType(weapon.Name));
    }
}

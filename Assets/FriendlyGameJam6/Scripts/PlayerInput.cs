using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelectionType
{
    Citizen = 0,
    Weapon = 1,
    Location = 2,
}

public class PlayerInput : MonoBehaviour
{
    public object SelectedObject;

    [HideInInspector]
    public SelectionType SelectedType;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Here because I don't wanna make events
            object previousSelectedObject = SelectedObject;
            SelectionType previousSelectedType = SelectedType;

            if (HandleSelectionInput<Citizen>(SelectionType.Citizen))
            {
                if (previousSelectedObject != null)
                {
                    if (previousSelectedType == SelectionType.Weapon)
                    {
                        // Equip the weapon
                        GameObject player = SelectedObject as GameObject;
                        player.GetComponent<Citizen>().EquipWeapon(previousSelectedObject as CitizenWeapon);
                    }
                }
            }
            else if (HandleSelectionInput<Location>(SelectionType.Location))
            {
                if (previousSelectedObject != null)
                {
                    if (previousSelectedType == SelectionType.Citizen)
                    {
                        // Equip the weapon
                        GameObject player = previousSelectedObject as GameObject;
                        GameObject location = SelectedObject as GameObject;
                        player.GetComponent<Citizen>().UpdateLocation(location.transform.position);
                    }
                }
            }
            else
            {
                SelectedObject = null;
            }
        }
    }

    public void SetSelection(object go, SelectionType type)
    {
        SelectedObject = go;
        SelectedType = type;
    }

    private bool HandleSelectionInput<ObjectType>(SelectionType type)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.attachedRigidbody != null)
            {
                ObjectType citizen = hitInfo.collider.attachedRigidbody.GetComponent<ObjectType>();
                if (citizen != null)
                {
                    SetSelection(hitInfo.collider.attachedRigidbody.gameObject, type);
                    return true;
                }
            }
        }
        return false;
    }
}

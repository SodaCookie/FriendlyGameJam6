using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameObject SelectedCitizen;

    // Update is called once per frame
    void Update()
    {
        UpdateMoveSelect();
        UpdateCitizenSelect();
    }

    private void UpdateMoveSelect()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.GetComponent<Citizen>())
            {
                SelectedCitizen = hitInfo.collider.gameObject;
            }
        }
        else
        {
            SelectedCitizen = null;
        }
    }

    private void UpdateCitizenSelect()
    {
        // TO IMPLEMENT
    }
}

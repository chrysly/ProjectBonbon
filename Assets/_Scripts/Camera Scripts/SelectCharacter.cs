using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SelectCharacter : MonoBehaviour
{
    private Transform selected;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, 100)) {
            
                if (hit.collider.gameObject.tag == "Character") {
                    selected = hit.transform;   
                } else {
                    selected = null;
                }
            }   
        }
    }

    public Transform getSelected() {
        return selected;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SelectCharacter : MonoBehaviour
{
    private CharacterActor selected;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, 100)) {
                Transform target = hit.transform;
                if (target.gameObject.tag == "Character") {
                    if (selected != null) {
                        selected.transform.GetChild(1).gameObject.SetActive(false);   
                    }
                    selected = hit.transform.gameObject.GetComponent<CharacterActor>();
                    selected.transform.GetChild(1).gameObject.SetActive(true);
                } else {
                    if (selected != null) {
                        selected.transform.GetChild(1).gameObject.SetActive(false);   
                    }
                    selected = null;
                }
            }   
        }
    }

    public CharacterActor getSelected() {
        return selected;
    }
}

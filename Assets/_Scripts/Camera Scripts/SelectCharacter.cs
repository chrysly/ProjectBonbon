using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class SelectCharacter : MonoBehaviour
{
    [SerializeField] private CharacterActor selected;
    [SerializeField] private CameraController cam;
    [SerializeField] private GameObject[] healthbars;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && selected != null) {
            deselectUI();
        }
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, 100)) {
                Transform target = hit.transform;
                if (target.gameObject.tag == "Character") {
                    CharacterActor temp = target.gameObject.GetComponent<CharacterActor>();
                    if (selected == null) {
                        if (cam.getIsCharCam()) {
                            selected = temp;
                            selectUI();   
                        } else {
                            selected = temp;
                            selectCharacter();
                        }
                    } else if (selected != null){
                        if (cam.getIsCharCam()) {
                            if (selected == temp) {
                                deselectUI();
                            } else {
                                deselectUI();
                                selected = temp;
                                selectUI();
                            }   
                        } else {
                            if (selected == temp) {
                                deselectCharacter();
                                selected = null;
                            } else {
                                deselectCharacter();
                                selected = temp;
                                selectCharacter();
                            }   
                        }
                    }
                }
            }   
        }
    }
    void selectCharacter() {
        selected.transform.GetChild(2).gameObject.SetActive(true);
    }
    void selectUI() {
        selectCharacter();
        selected.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        selected.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
    }
    void deselectCharacter() {
        selected.transform.GetChild(2).gameObject.SetActive(false);
    }
    public void deselectUI() {
        deselectCharacter();
        selected.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
        selected.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        selected = null;
    }
    public void showHealthbars() {
        foreach (GameObject healthbar in healthbars) {
            healthbar.SetActive(true);
        }
    }
    public void hideHealthbars() {
        foreach (GameObject healthbar in healthbars) {
            healthbar.SetActive(false);
        }
    }
    public CharacterActor getSelected() {
        return selected;
    }
}

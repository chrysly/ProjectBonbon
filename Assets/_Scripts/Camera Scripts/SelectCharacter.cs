using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Events;

public class SelectCharacter : MonoBehaviour
{
    [SerializeField] private CharacterActor selected;
    [SerializeField] private CameraController cam;
    [SerializeField] private GameObject[] healthbars;

    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;

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
                                fadeOut = true;
                                FunctionTimer.Create(deselectUI, .1f);
                            } else {
                                fadeOut = true;
                                FunctionTimer.Create(deselectUI, .05f);
                                StartCoroutine(setSelected(temp));
                                FunctionTimer.Create(selectUI, 1.25f);
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
        
        if (fadeIn) {
            CanvasGroup canvas = selected.transform.GetChild(1).GetChild(0).gameObject.GetComponent<CanvasGroup>();
            if (canvas.alpha < 1) {
                canvas.alpha += Time.deltaTime;
                if (canvas.alpha >= 1) {
                    fadeIn = false;
                }
            }
        }
        if (fadeOut) {
            CanvasGroup canvas = selected.transform.GetChild(1).GetChild(0).gameObject.GetComponent<CanvasGroup>();
            if (canvas.alpha > 0) {
                canvas.alpha -= Time.deltaTime * 8;
                if (canvas.alpha <= 10) {
                    canvas.alpha = 0;
                    fadeOut = false;
                }
            }
        }
    }
    void selectCharacter() {
        selected.transform.GetChild(2).gameObject.SetActive(true);
    }
    void selectUI() {
        selectCharacter();
        showUI();
        selected.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
    }
    void deselectCharacter() {
        selected.transform.GetChild(2).gameObject.SetActive(false);
    }
    public void deselectUI() {
        hideUI();
        deselectCharacter();
        selected.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        fadeOut = false;
        selected = null;
    }
    void showUI() {
        fadeIn = true;
    }
    void hideUI() {
        fadeOut = true;
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
    public IEnumerator setSelected(CharacterActor selected) {
        yield return new WaitForSeconds(.1f);
        this.selected = selected;
    }
}

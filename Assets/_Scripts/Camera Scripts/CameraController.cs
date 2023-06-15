using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private SelectCharacter selector;
    
    [SerializeField] private CinemachineVirtualCamera aerialCam;
    [SerializeField] private CinemachineVirtualCamera charCam;
    
    [SerializeField] private GameObject AerialUI;
    

    private void Start() {
        charCam.gameObject.SetActive(true);
        aerialCam.gameObject.SetActive(false);
        AerialUI.SetActive(false);
    }

    void Update()
    {
        if (selector.getSelected() != null) {
            charCam.m_Follow = selector.getSelected().transform.GetChild(0);
        }
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (aerialCam.isActiveAndEnabled) {
                selector.showHealthbars();
                charCam.gameObject.SetActive(true);
                aerialCam.gameObject.SetActive(false);
                AerialUI.SetActive(false);
            } else {
                if (selector.getSelected() != null) {
                    selector.setFadeOut(true);
                    FunctionTimer.Create(selector.deselectUI, .02f);
                    selector.StartCoroutine(selector.setSelected(null));
                }
                FunctionTimer.Create(selector.hideHealthbars, .03f);
                AerialUI.SetActive(true);
                aerialCam.gameObject.SetActive(true);
                charCam.gameObject.SetActive(false);
            }
        }
    }
    
    public bool getIsCharCam() {
        return charCam.isActiveAndEnabled;
    }
}

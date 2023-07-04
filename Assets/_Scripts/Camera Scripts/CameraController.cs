using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour {
    [SerializeField] private SelectManager selector;

    [SerializeField] private CinemachineVirtualCamera aerialCam;
    [SerializeField] private CinemachineVirtualCamera charCam;

    [SerializeField] private GameObject AerialUI;

    private Transform focus = null;

    public delegate void SwitchView(bool isAerial);
    public event SwitchView OnSwitchView;

    private void Start() {
        charCam.gameObject.SetActive(true);
        aerialCam.gameObject.SetActive(false);
        AerialUI.SetActive(false);
        RegisterEvents();
    }

    private void RegisterEvents() {
        selector.OnSelect += FocusCamera;
        selector.OnDeselect += ClearFocus;
    }

    void Update()
    {
        if (focus != null) {
            charCam.m_Follow = focus.transform.GetChild(0);
        } else {
            charCam.m_Follow = null;    //Does not follow anything
        }

        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (aerialCam.isActiveAndEnabled) {
                charCam.gameObject.SetActive(true);
                aerialCam.gameObject.SetActive(false);
                AerialUI.SetActive(false);
                OnSwitchView.Invoke(false);
            } else {
                /*if (focus != null) {
                    selector.setFadeOut(true);
                    FunctionTimer.Create(selector.deselectUI, .02f);
                    selector.StartCoroutine(selector.setSelected(null));
                }*/
                OnSwitchView.Invoke(true);
                AerialUI.SetActive(true);
                aerialCam.gameObject.SetActive(true);
                charCam.gameObject.SetActive(false);
            }
        }
    }

    private void FocusCamera(CharacterActor actor) {
        focus = actor.transform;
    }
    
    private void ClearFocus() {
        focus = null;
        Debug.Log("focus cleared");
    }
    
    public bool getIsCharCam() {
        return charCam.isActiveAndEnabled;
    }
}

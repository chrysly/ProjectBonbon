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

    private void Start() {
        charCam.gameObject.SetActive(false);
        aerialCam.gameObject.SetActive(true);
    }

    void Update()
    {
        if (selector.getSelected() != null) {
            charCam.m_Follow = selector.getSelected().transform.GetChild(0);
        }
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (aerialCam.isActiveAndEnabled) {
                charCam.gameObject.SetActive(true);
                aerialCam.gameObject.SetActive(false);
            } else {
                aerialCam.gameObject.SetActive(true);
                charCam.gameObject.SetActive(false);
            }
        }
    }
}

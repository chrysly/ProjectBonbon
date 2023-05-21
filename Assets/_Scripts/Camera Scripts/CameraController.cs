using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform character1;
    [SerializeField] private Transform character2;

    private bool isOnChar1;

    [SerializeField] private CinemachineVirtualCamera cam;
    // Start is called before the first frame update
    private void Start()
    {
        isOnChar1 = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isOnChar1) {
                cam.m_Follow = character2;
                isOnChar1 = false;
            } else {
                cam.m_Follow = character1;
                isOnChar1 = true;
            }
        }
    }
}

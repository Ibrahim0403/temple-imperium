﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public Transform playerObject;

    public float mouseSens = 100f;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //clamp rotation so limits how far player can rotate

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //Quaternion used to handle rotations
        playerObject.Rotate(Vector3.up * mouseX);
    }
}

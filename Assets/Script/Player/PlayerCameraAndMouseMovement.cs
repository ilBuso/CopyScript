using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraAndMouseMovement : MonoBehaviour
{
    //Script
    SettingsMenu settingsMenu;

    //Player
    private GameObject player;

    //Mouse
    private float xRotation;
    private Vector3 mouse;
    private bool isPaused; //variable from ScreenButtons

    private float mouseSensitivity;

    void Start()
    {
        //Assign
        player = GameObject.FindGameObjectWithTag("Player");

        xRotation = 0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mouseSensitivity = 100f;
    }

    void Update()
    {
        //Assign
        isPaused = ScreenButtons.isPaused;
        //mouseSensitivity = settingsMenu.SetSensitivity();

        //Mouse
        if (!isPaused)
        {
            //for the input a vector3(0f, 0f, 0f) is created in for x and y is putted the mouse input so
            //we have all the mouse inputs in a single variable
            mouse = Vector3.zero;
            mouse.x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            mouse.y = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            //difficolt math
            xRotation -= mouse.y;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            player.transform.Rotate(Vector3.up * mouse.x);
        }
    }
}
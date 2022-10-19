using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{

    public float mouseSens = 250f;
    public Transform playerBody;
    float xRotation = 0f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Locks cursor to window
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime; //Move mouse independant of framerate
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, 20f, 20f); // Locks the X angle to only 20 degrees

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //Rotates based on movement
        playerBody.Rotate(Vector3.up * mouseX);

    }
}

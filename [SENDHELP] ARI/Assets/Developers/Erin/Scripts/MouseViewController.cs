using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseViewController : MonoBehaviour
{
    //
    // [ 3.30.2020 ] Erin Alvarico
    // To create a script to allow camera to follow mouse
    //

    // VARIABLES
    public float mouseSensitivity = 50.0f; // Stores mouse sensitivity
   // public Transform playerBody; // Stores player body position & location               //NOT NEEDED
    float xRotation = 0f; // Rotation factor on x-axis
    float yRotation = 0f;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // Gets the movement of x-axis * the sensitivity value stored. Time.deltaTime normalize function by * action with time of fps of computer
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // Gets the movement of y-axis * the sensitivity value stored.

        xRotation = xRotation - mouseY; // Temp Storage of variable of position of camera
        yRotation = yRotation - mouseX;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f); // Rotational actions: utilize Quaternion.Euler(x,y,z)
        //playerBody.Rotate(Vector3.up * mouseY); //player object references will rotate with camera by vector and mouse x-axis values             //NOT NEEDED

    }
}

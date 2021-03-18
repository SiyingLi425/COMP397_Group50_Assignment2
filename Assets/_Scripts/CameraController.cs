using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Vincent Tse.
 * 2021-02-13
 */

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 5.0f;
    public Transform playerBody;
    public Joystick joystick;

    private float XRotation = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.GamePaused)
        {

            float x = joystick.Horizontal;
            float z = joystick.Vertical;

            float mouseX = x * mouseSensitivity;
            float mouseY = z * mouseSensitivity;


            XRotation -= mouseY;
            XRotation = Mathf.Clamp(XRotation, 0.0f, 20.0f);

            transform.localRotation = Quaternion.Euler(XRotation, 0.0f, 0.0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float MouseSensi = 100f;
    public Transform Tank;

    float xRotation = 0;
    // Start is called before the first frame update
    void Start()
    {
      //  Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensi * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensi * Time.deltaTime;

        Tank.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

       // xRotation -= mouseY;
       // xRotation = Mathf.Clamp
    }
}

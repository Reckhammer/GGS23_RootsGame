using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabLook : MonoBehaviour
{
    ////////////////////////Camera Mouse Look Variables
    public float lookspeed;
    public float MinClampX, MaxClampX, MinClampY, MaxClampY;
    [HideInInspector]
    public float rotY;
    [HideInInspector]
    public float rotX;
    ////////////////////////Camera Mouse Look Variables

    private void Start()
    {
        rotY = transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = -Input.GetAxisRaw("Mouse Y") * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse X") * Time.deltaTime;

        rotY += mouseY * lookspeed;
        rotX += mouseX * lookspeed;

        rotX = Mathf.Clamp(rotX, -MinClampX, MaxClampX);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotX, rotY, 0), 20 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (GetComponent<Camera>().fieldOfView == 60)
            {
                GetComponent<Camera>().fieldOfView = 30;
            }
            else
            {
                GetComponent<Camera>().fieldOfView = 60;
            }
        }
    }
}

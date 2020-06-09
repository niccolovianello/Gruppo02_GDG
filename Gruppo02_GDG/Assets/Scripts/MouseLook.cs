using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float cameraFollowHeadSpeed= 100f;
    public Transform playerBody;
    public Transform headTarget;
    public Transform hand;
    public bool haveBow= false;
    

    float xRotation = 0f;
   
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        float mouseX = Input.GetAxis("Mouse X")* mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y")* mouseSensitivity* Time.deltaTime;


        
       
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        Vector3 dPos = headTarget.position;
        
        Vector3 sPos = Vector3.Lerp(transform.position, dPos, cameraFollowHeadSpeed * Time.deltaTime);
       
        transform.position = sPos;

        if (haveBow == true)
        {
            Debug.Log("mira");
            //transform.localRotation();
        }





    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float cameraFollowHeadSpeed= 10000;
    public Transform playerBody;
    public Transform headTarget;
    public Transform pointHead;

    public bool haveBow= false;
    public Vector3 of = new Vector3 ( 0,0,0.1f);
    

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

       
        
        
       
        transform.position = pointHead.position;

        if (haveBow == true)
        {
            Debug.Log("mira");
            transform.Rotate(new Vector3 (0,75,0),Space.World);
            
        }







    }
}

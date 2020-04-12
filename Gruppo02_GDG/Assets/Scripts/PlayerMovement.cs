using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -19.62f;
    public float jumpHeight = 3f;
    private bool isSprinting;
    public float sprintModifier;
   
    private float movementCounter;
    private float idleCounter;

    public Camera normalCam;
    public Transform armParent;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private float baseFOV;
    private float sprintFOVModifier = 1.25f;

    private Vector3 targetArmBobPosition;
    private Vector3 armParentOrigin;
    Vector3 velocity;
    bool isGrounded;
    #endregion

    #region MonoBehaviour CallBacks

    private void Start()
    {
        baseFOV = normalCam.fieldOfView;
        isSprinting = false;
        armParentOrigin = armParent.localPosition;
    }
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
       
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
       
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool sprint = Input.GetKey(KeyCode.LeftShift);
        bool isSprinting = sprint && z>0;


        float t_adjustedSpeed = speed;
        if (isSprinting)
        {
            t_adjustedSpeed *= sprintModifier;
        }

        

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * t_adjustedSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

       
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (isSprinting)
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintModifier, Time.deltaTime * 8f);
        }
        else
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV, Time.deltaTime * 8f);
        }

        if (x == 0 && z == 0)
        {
            HeadBob(idleCounter, 0.025f, 0.025f);
            idleCounter += Time.deltaTime;
            armParent.localPosition = Vector3.Lerp(armParent.localPosition, targetArmBobPosition, Time.deltaTime * 2f);
        }
        else if(!isSprinting)
        {
            HeadBob(movementCounter, 0.035f, 0.035f);
            movementCounter += Time.deltaTime * 3f;
            armParent.localPosition = Vector3.Lerp(armParent.localPosition, targetArmBobPosition, Time.deltaTime * 6f);
        }
        else 
        {
            HeadBob(movementCounter, .15f, 0.075f);
            movementCounter += Time.deltaTime * 7f;
            armParent.localPosition = Vector3.Lerp(armParent.localPosition, targetArmBobPosition, Time.deltaTime * 10f);
        }




    }
    #endregion

    #region Private Methods

    void HeadBob(float p_z,float p_x_intensity,float p_y_intensity)
    {
        targetArmBobPosition = armParentOrigin + new Vector3(Mathf.Cos(p_z)*p_x_intensity, Mathf.Sin(p_z * 2)*p_y_intensity,0);
    }
    #endregion
}


﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -19.62f;
    public float jumpHeight = 3f;
    public bool isSprinting;
    public float sprintModifier;
    public float sprintModifierVelocity = 2;
    public float maxStamina = 100;
    public float currentStamina;
    public float ratioStaminaDischarge = 15f;
    public float ratioStaminaCharge = 10f;
    public bool blockSprint = false;
    public float refractoryTime = 3f;
    
   
    private float movementCounter;
    private float idleCounter;

    public Camera normalCam;
    public Transform armParent;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Animator JhonnyAnimator;
    public AudioManager aud;

    private float baseFOV;
    private float sprintFOVModifier = 1.1f;

    private Vector3 targetArmBobPosition;
    private Vector3 armParentOrigin;
    Vector3 velocity;
    bool isGrounded;
    #endregion

    #region MonoBehaviour CallBacks

    private void Start()
    {
        aud = FindObjectOfType<AudioManager>();
        baseFOV = normalCam.fieldOfView;
        isSprinting = false;
        armParentOrigin = armParent.localPosition;
        currentStamina = maxStamina;
        //JhonnyAnimator = FindObjectOfType<Animator>();
       // Debug.Log(JhonnyAnimator);
    }
    void Update()
    {
        //JhonnyAnimator.speed = 
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
       
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x;
        float z;

        if (JhonnyAnimator.GetBool("Aim") == true && JhonnyAnimator.GetBool("HaveBow") == true)
        {
            x = Input.GetAxis("Vertical");
            z = -Input.GetAxis("Horizontal");

        }
        else
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");

        }

        bool sprint = Input.GetKey(KeyCode.LeftShift);
        
        isSprinting = sprint && z>0 && isGrounded;

        
        float t_adjustedSpeed = speed;
        if (isSprinting && currentStamina > 0 && blockSprint == false)
        {
            
            t_adjustedSpeed *= sprintModifierVelocity;
            JhonnyAnimator.SetFloat("Speed", 15);
            currentStamina -= ratioStaminaDischarge * Time.deltaTime;
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * 8f);
            //HeadBob(movementCounter, .15f, 0.075f);
            movementCounter += Time.deltaTime * 7f;
            armParent.localPosition = Vector3.Lerp(armParent.localPosition, targetArmBobPosition, Time.deltaTime * 10f);

            if (currentStamina <= 0)
            {
                blockSprint = true;
                aud.Play("DeepBreathe");
            } 
        }
        if(!isSprinting || currentStamina < 0 || blockSprint ==true)
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV, Time.deltaTime * 8f);
            JhonnyAnimator.SetFloat("Speed", 5);
            currentStamina += ratioStaminaCharge * Time.deltaTime;
            StartCoroutine(BlockSprintMethod(refractoryTime));
            
        }

        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }

        if (z == 0 && x == 0)
        {
            JhonnyAnimator.SetFloat("Speed", 0);
        }
        

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * t_adjustedSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            aud.Play("Jump");
            JhonnyAnimator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            JhonnyAnimator.SetTrigger("Jump");
        }

       
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        
        

        if (x == 0 && z == 0)
        {
            //HeadBob(idleCounter, 0.025f, 0.025f);
            idleCounter += Time.deltaTime;
            armParent.localPosition = Vector3.Lerp(armParent.localPosition, targetArmBobPosition, Time.deltaTime * 2f);
        }
        else if(!isSprinting)
        {
            //HeadBob(movementCounter, 0.035f, 0.035f);
            movementCounter += Time.deltaTime * 3f;
            armParent.localPosition = Vector3.Lerp(armParent.localPosition, targetArmBobPosition, Time.deltaTime * 6f);
        }


        if (Input.GetMouseButtonDown(1))
        {
            if (JhonnyAnimator.GetBool("Aim") == false)
                JhonnyAnimator.SetBool("Aim", true);
            else
                JhonnyAnimator.SetBool("Aim", false);
        }
      


    }
    #endregion

    #region Private Methods

    //void HeadBob(float p_z,float p_x_intensity,float p_y_intensity)
    //{
    //    targetArmBobPosition = armParentOrigin + new Vector3(Mathf.Cos(p_z)*p_x_intensity, Mathf.Sin(p_z * 2)*p_y_intensity,0);
    //}

    IEnumerator BlockSprintMethod(float timeRest)
    {
        yield return new WaitForSeconds(timeRest);
        blockSprint = false;
    }
    #endregion
}


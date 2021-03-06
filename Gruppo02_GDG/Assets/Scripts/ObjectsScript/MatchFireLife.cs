﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Animations;

namespace Com.Kawaiisun.SimpleHostile
{
    public class MatchFireLife : MonoBehaviour
    {
        public bool isOn;
        public ParticleSystem fire;
        private ParticleSystem[] firech;
        public Light fireLight;
        
        public Equipment match;
        private ObjectsManagement obj;
        private Animator JhonnyAnimator;
        private AudioManager aud;
        public float currentTimeOfMatchLife;
        public float decrementRate = 0.5f;
        private PlayerMovement movePlayer;

        private float fireTimeLeftTot;
        private float fireTimeLeft;
        private float startRate = 0f;

        public UIScript UI;

        private void Awake()
        {
            
            UI = GameObject.Find("CanvasUI").GetComponent<UIScript>();
            if (UI == null)
                Debug.Log("not found UI from match");
            //Debug.Log(UI.name);
        }

        private void Start()
        {
            movePlayer = FindObjectOfType<PlayerMovement>();
            decrementRate = match.rateDecrement;
            aud = FindObjectOfType<AudioManager>();
            currentTimeOfMatchLife = match.charge;
            isOn = false;
            fire.Stop();
            fireLight.enabled = false;
            obj = FindObjectOfType<ObjectsManagement>();
            JhonnyAnimator = obj.JhonnyAnimator;
            firech = fire.gameObject.GetComponentsInChildren<ParticleSystem>();
            fireTimeLeftTot = currentTimeOfMatchLife / 3;
            Debug.Log(JhonnyAnimator);
                //15f;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                aud.Play("Match");
                obj.ammo[0]--;
                Debug.Log(obj.ammo[0]);
                isOn = !isOn;
                if (isOn)
                {
                    fireLight.enabled = true;
                    fire.Play();
                }
                else
                {
                    /*fireLight.enabled = false;
                    fire.Stop();*/
                    UI.ActiveWeapon(4);
                    UI.UpdateResources("Matches", -1);

                    Destroy(obj.getCurrentObj());
                    JhonnyAnimator.SetBool("HaveTorch", false);
                    Debug.Log(JhonnyAnimator.GetBool("HaveTorch"));

                }
            }

            if(isOn)
            {
                currentTimeOfMatchLife -= decrementRate * Time.deltaTime;

                // fadelight

                if (currentTimeOfMatchLife <= fireTimeLeftTot)
                {
                    fireLight.DOIntensity(0f, fireTimeLeftTot);
                    for (int i = 0; i < firech.Length; i++)
                    {
                        if (startRate == 0f)
                        {
                            startRate = firech[i].emission.rateOverTime.Evaluate(1f);
                            //Debug.Log("startrate assigned value: " + startRate);
                            fireTimeLeft = fireTimeLeftTot;
                        }
                        if (firech[i].emission.rateOverTime.Evaluate(1f) > 0)
                        {
                            var emission = firech[i].emission;
                            emission.rateOverTime = Mathf.Clamp(Mathf.Lerp(0f, startRate, fireTimeLeft / fireTimeLeftTot), 0f, startRate);
                            //Debug.Log("Emission: " + firech[i].emission.rateOverTime.Evaluate(1f) + "firetimeleft: " + fireTimeLeft);
                        }
                    }
                    fireTimeLeft -= decrementRate * Time.deltaTime;
                }
                if (movePlayer.isSprinting == true)
                {
                    UI.ActiveWeapon(4);
                    UI.UpdateResources("Matches", -1);

                    Destroy(obj.getCurrentObj());
                    JhonnyAnimator.SetBool("HaveTorch", false);
                    Debug.Log(JhonnyAnimator.GetBool("HaveTorch"));
                    obj.MatchLightMethod();
                }


                //end fadelight
            }

            if (currentTimeOfMatchLife <= 0)
            {
                UI.ActiveWeapon(4);
                UI.UpdateResources("Matches", -1);

                obj.JhonnyAnimator.SetBool("HaveTorch", false);
                Destroy(obj.getCurrentObj());
                obj.MatchLightMethod();
                
            }

            UI.SetMLife(currentTimeOfMatchLife);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.Kawaiisun.SimpleHostile
{
    public class FlashLight : MonoBehaviour
    {

        private Light flashlight;
        public bool isOn = false;
        public Equipment selectionFlashlight;
        public ObjectsManagement obj;
        public float currentBatteryEnergy;
        public float dischargeBatteryVelocity = 0.5f;
        private SupportScriptResources ssr;
        private PlayerCombatScript pl;
        private AudioManager aud;
        float startIntensity;

        public UIScript UI;
        //public UISlot UISlot;

        private void Awake()
        {
            
            UI = GameObject.Find("CanvasUI").GetComponent<UIScript>();
            if (UI == null)
                Debug.Log("not found UI from flashlight");
            //UISlot = GameObject.Find("CanvasUI").GetComponentInChildren<UISlot>();
            //if (UISlot == null)
            //    Debug.Log("not found UISlot from flashlight");
            //Debug.Log(UI.name);
        }

        private void Start()

        {
            aud = FindObjectOfType<AudioManager>();
            ssr = FindObjectOfType<SupportScriptResources>();
            obj = FindObjectOfType<ObjectsManagement>();
            flashlight = this.GetComponent<Light>();
            //currentBatteryEnergy = 10f;
            currentBatteryEnergy = ssr.GetRemainEnergy();
            pl = FindObjectOfType<PlayerCombatScript>();
            startIntensity = flashlight.intensity;
            isOn = true;
            pl.isOn = true;
            dischargeBatteryVelocity = selectionFlashlight.rateDecrement;
        }
        void Update()
        {
            if (obj.getCurrentObj() == null)
                return;

            if (Input.GetKeyDown(KeyCode.R) && selectionFlashlight.isSelected == true)
            {

                if (obj.ammo[2] > 0)
                {
                    aud.Play("ReloadFlashlight");
                    Debug.Log("Ricarica!");
                    ssr.SetRemainEnergy(selectionFlashlight.charge);
                    currentBatteryEnergy = ssr.GetRemainEnergy();
                    obj.ammo[2]--;

                    UI.UpdateResources("Battery", -1);
                }
                else
                {
                    Debug.Log("No Batteries left!");
                }
                
            }
            if (selectionFlashlight.isSelected == true)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    isOn = !isOn;
                    pl.isOn = isOn;
                    aud.Play("Flashlight");

                    if (isOn)
                    {
                        currentBatteryEnergy = ssr.GetRemainEnergy();

                    }
                    else
                        ssr.SetRemainEnergy(currentBatteryEnergy);
                }
                if (isOn)
                {

                    if (currentBatteryEnergy >= 0)
                    {
                        flashlight.enabled = true;
                        currentBatteryEnergy -= dischargeBatteryVelocity * Time.deltaTime;

                        //flicker

                        //Debug.Log(currentBatteryEnergy + " + " + flashlight.intensity + " + " + currentBatteryEnergy/20f);
                        if ((currentBatteryEnergy / 20f) < /*1.5f*/ startIntensity)
                        {
                            flashlight.intensity = Random.Range(Random.Range((currentBatteryEnergy / 20f), /*1.5f*/ startIntensity), /*1.5f*/ startIntensity);
                        }

                        //end flicker
                    }
                    else 
                    {
                        flashlight.enabled = false;
                        
                    }
                        

                }
                else
                {
                    flashlight.enabled = false;
                    
                } 
            }
            else
            {
                flashlight.enabled = false;
                ssr.SetRemainEnergy(currentBatteryEnergy);
            }

            UI.SetFBattery(currentBatteryEnergy);
            ssr.SetRemainEnergy(currentBatteryEnergy);
        }
    }
}
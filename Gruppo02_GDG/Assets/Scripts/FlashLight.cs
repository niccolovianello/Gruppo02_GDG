using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.Kawaiisun.SimpleHostile
{
    public class FlashLight : MonoBehaviour
    {

        private Light flashlight;
        private bool isOn = false;
        public Equipment selectionFlashlight;
        public ObjectsManagement obj;
        public float currentBatteryEnergy;
        public float maxEnergySingleBattery;
        public float dischargeBatteryVelocity = 0.5f;

        float startIntensity;

        private void Start()
        {
            obj = FindObjectOfType<ObjectsManagement>();
            flashlight = this.GetComponent<Light>();
            currentBatteryEnergy = selectionFlashlight.charge;

            startIntensity = flashlight.intensity;
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {

                if (obj.ammo[2] > 0)
                {
                    currentBatteryEnergy = maxEnergySingleBattery;
                    obj.ammo[2]--;
                }
                else
                {
                    Debug.Log("No Batteries left!");
                }
                
            }
            if (selectionFlashlight == true)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    isOn = !isOn;
                }
                if (isOn && currentBatteryEnergy >= 0)
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
                    flashlight.enabled = false;
            }
            else
            {
                flashlight.enabled = false;
            }

            
           
        }
    }
}
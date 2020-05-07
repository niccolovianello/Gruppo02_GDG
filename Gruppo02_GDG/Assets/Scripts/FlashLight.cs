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


        private void Start()
        {
            obj = FindObjectOfType<ObjectsManagement>();
            flashlight = this.GetComponent<Light>();
            currentBatteryEnergy = selectionFlashlight.charge;
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
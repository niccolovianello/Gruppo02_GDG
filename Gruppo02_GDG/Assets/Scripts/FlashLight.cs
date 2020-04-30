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

        private void Start()
        {
            flashlight = this.GetComponent<Light>();
        }
        void Update()
        {
            if (selectionFlashlight == true)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    isOn = !isOn;
                }
                if (isOn)
                    flashlight.enabled = true;
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
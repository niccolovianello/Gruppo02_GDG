using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Kawaiisun.SimpleHostile
{
    public class TorchOnOff : MonoBehaviour
    {

        public bool isOn;
        public ParticleSystem fire;
        public Light fireLight;
        public Equipment torch;
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (torch.isSelected == true)
                {
                    if (isOn)
                    {
                        fireLight.enabled = true;
                        fire.Play();
                        isOn = true;
                    }
                    else
                    {
                        fireLight.enabled = false;
                        fire.Stop();
                        isOn = false;
                    }
               

                }
                else
                {
                    fireLight.enabled = false;
                    fire.Stop();
                    isOn = false;
                }

            }
        }
    }
}

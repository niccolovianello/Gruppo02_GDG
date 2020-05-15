using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Kawaiisun.SimpleHostile
{
    public class MatchFireLife : MonoBehaviour
    {
        public bool isOn;
        public ParticleSystem fire;
        public Light fireLight;
        
        public Equipment match;
        private ObjectsManagement obj;
        public float currentTimeOfMatchLife;
        public float decrementRate = 0.5f;
        private void Start()
        {
            currentTimeOfMatchLife = match.charge;
            isOn = false;
            fire.Stop();
            fireLight.enabled = false;
            obj = FindObjectOfType<ObjectsManagement>();           
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
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
                    Destroy(obj.getCurrentObj());
                }
            }

            if(isOn)
            {
                currentTimeOfMatchLife -= decrementRate * Time.deltaTime;
            }

            if (currentTimeOfMatchLife <= 0)
            {
                
                
                Destroy(obj.getCurrentObj());
            }
        }
    }
}
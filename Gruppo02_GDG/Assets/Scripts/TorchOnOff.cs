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
        private ObjectsManagement obj;
        public float timeOfTorchLife = 50f;
        public float currentTimeOfTorchLife;
        public float decrementRate = 0.5f;

        private void Start()
        {
            currentTimeOfTorchLife = timeOfTorchLife;
            isOn = false;
            obj = FindObjectOfType<ObjectsManagement>();
        }
        void Update()
        {
            if (torch.isSelected == true)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                Debug.Log(torch.isSelected);
                
                isOn = !isOn;

                    if (isOn && obj.ammo[0] > 0)
                    {
                        fireLight.enabled = true;
                        fire.Play();
                        obj.ammo[0]--;

                    }
                    else
                    {
                        Debug.Log("Sono finiti i fiammiferi");
                    }


                }
                if (!isOn)
                {

                    fireLight.enabled = false;
                    fire.Stop();
                }



                if (isOn)
                {
                    currentTimeOfTorchLife -= decrementRate * Time.deltaTime;

                    //fadelight

                    

                    //end fadelight
                }
                if (currentTimeOfTorchLife <= 0)
                {
                    Destroy(this);
                    int i = obj.getCurrentIndex();
                    obj.pickLoadout[i] = null;
                }


            }

           
            
        }
    }
}

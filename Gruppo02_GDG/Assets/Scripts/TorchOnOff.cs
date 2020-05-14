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
        public float currentTimeOfTorchLife;
        public float decrementRate = 0.5f;
        private SupportScriptResources ssr;

        float startIntensity;

        private void Start()
        {
            ssr = FindObjectOfType<SupportScriptResources>();
            currentTimeOfTorchLife = torch.charge;
            isOn = false;
            obj = FindObjectOfType<ObjectsManagement>();

            startIntensity = fireLight.intensity;
        }
        void Update()
        {
            if (torch.isSelected == true)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Debug.Log(torch.isSelected);

                    isOn = !isOn;

                    if (obj.getCurrentObj() == null)
                        return;

                    if (isOn)
                    {
                        currentTimeOfTorchLife = ssr.GetRemainLifeTorch();
                        if (obj.ammo[0] > 0)
                        {
                            fireLight.enabled = true;
                            fire.Play();
                            obj.ammo[0]--;
                        }
                       

                    }
                    else
                    {
                        ssr.SetRemainLifeTorch(currentTimeOfTorchLife);
                        if (obj.ammo[0] == 0)
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

                    if ((currentTimeOfTorchLife / 10f) < /*3f*/ startIntensity)
                    {
                        fireLight.intensity = Random.Range(Random.Range((currentTimeOfTorchLife / 10f), /*3f*/ startIntensity), /*3f*/ startIntensity);
                    }

                    //end fadelight
                }
                if (currentTimeOfTorchLife <= 0)
                {
                    
                    
                    int i = obj.getCurrentIndex();
                    obj.pickLoadout[i] = null;
                    Destroy(obj.getCurrentObj());
                }


            }
            if(torch.isSelected == false)
            {
                ssr.SetRemainLifeTorch(currentTimeOfTorchLife);
            }

           
           
            
        }
    }
}

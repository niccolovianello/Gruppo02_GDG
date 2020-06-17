using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Com.Kawaiisun.SimpleHostile
{
    public class TorchOnOff : MonoBehaviour
    {
        
        public bool isOn;
        public ParticleSystem fire;
        private ParticleSystem[] firech;
        public Light fireLight;
        public Equipment torch;
        private ObjectsManagement obj;
        public float currentTimeOfTorchLife;
        public float decrementRate = 0.5f;
        private SupportScriptResources ssr;
        private AudioManager aud;
       
        

        private float fireTimeLeftTot;
        private float fireTimeLeft;
        private float startRate = 0f;

        public UIScript UI;
        //public UISlot UISlot;

        private void Awake()
        {
            UI = GameObject.Find("CanvasUI").GetComponent<UIScript>();
            if (UI == null)
                Debug.Log("not found UI from torch");
            //UISlot = GameObject.Find("CanvasUI").GetComponentInChildren<UISlot>();
            //if (UISlot == null)
            //    Debug.Log("not found UISlot from torch");
            //Debug.Log(UI.name);

        }

        private void Start()
        {
            //Debug.Log("StartTorcia");
            ssr = FindObjectOfType<SupportScriptResources>();
            currentTimeOfTorchLife = ssr.GetRemainLifeTorch();
            isOn = false;
            fire.Stop();
            fireLight.enabled = false;
            obj = FindObjectOfType<ObjectsManagement>();
            aud = FindObjectOfType<AudioManager>();
            firech = fire.gameObject.GetComponentsInChildren<ParticleSystem>();
            fireTimeLeftTot = currentTimeOfTorchLife / 3;
                //15f;
        }
        void Update()
        {
            if (torch.isSelected == true)
            {
                

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    //Debug.Log(torch.isSelected);
                    if (obj.ammo[0] > 0)
                    {
                        isOn = !isOn;

                    }
                    else
                        isOn = false;

                    if (obj.getCurrentObj() == null)
                        return;

                    if (isOn)
                    {
                        currentTimeOfTorchLife = ssr.GetRemainLifeTorch();

                        aud.Play("Torch");
                            fireLight.enabled = true;
                            fire.Play();
                            obj.ammo[0]--;
                        //Debug.Log(obj.ammo[0]);
                        UI.UpdateResources("Matches", -1);
                        
                       

                    }
                    else
                    {
                        aud.Stop("Torch");
                        fireLight.enabled = false;
                        fire.Stop();
                        ssr.SetRemainLifeTorch(currentTimeOfTorchLife);
                        if (obj.ammo[0] == 0)
                            Debug.Log("fiammiferi finiti");
                    }
                    


                }
                

                if (isOn)
                {
                    currentTimeOfTorchLife -= decrementRate * Time.deltaTime;
                    //Debug.Log(Mathf.Round(currentTimeOfTorchLife));

                    //fadelight

                    if (currentTimeOfTorchLife <= fireTimeLeftTot)
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

                    //end fadelight
                }
                if (currentTimeOfTorchLife <= 0)
                {


                    int i = obj.getCurrentIndex();
                    obj.pickLoadout[i].isSelected = false;
                    obj.pickLoadout[i] = null;
                    UI.ActiveWeapon(4);
                    UI.UpdateWeapons("", i);

                    obj.JhonnyAnimator.SetBool("HaveTorch", false);
                    aud.Stop("Torch");
                    ssr.SetRemainLifeTorch(torch.charge);
                    
                    Destroy(obj.getCurrentObj());
                    

                    
                }


            }
            if(torch.isSelected == false)
            {
                aud.Stop("Torch");
                ssr.SetRemainLifeTorch(currentTimeOfTorchLife);
                isOn = false;
            }

            UI.SetTLife(currentTimeOfTorchLife);
        }

       
    }
}

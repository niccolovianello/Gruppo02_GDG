using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Com.Kawaiisun.SimpleHostile
{
    public class LanternScript : MonoBehaviour
    {
        public bool isOn;
        public ParticleSystem fire;
        private ParticleSystem[] firech;
        public Light fireLight;
        public Equipment lantern;
        private ObjectsManagement obj;
        public float currentOilRemainTime;
        public float decrementRate = 2f;
        private SupportScriptResources ssr;

        float startIntensity;
        float[] startFireRate;

        private float fireTimeLeftTot;
        private float fireTimeLeft;
        private float startRate = 0f;

        public UIScript UI;
        //public UISlot UISlot;

        private void Awake()
        {
            UI = GameObject.Find("CanvasUI").GetComponent<UIScript>();
            if (UI == null)
                Debug.Log("not found UI from lantern");
            //UISlot = GameObject.Find("CanvasUI").GetComponentInChildren<UISlot>();
            //if (UISlot == null)
            //    Debug.Log("not found UISlot from lantern");
            //Debug.Log(UI.name);

        }

        private void Start()
        {
            ssr = FindObjectOfType<SupportScriptResources>();
            currentOilRemainTime = ssr.GetRemainOil();
            isOn = false;
            fire.Stop();
            fireLight.enabled = false;
            obj = FindObjectOfType<ObjectsManagement>();

            firech = fire.gameObject.GetComponentsInChildren<ParticleSystem>();
            fireTimeLeftTot = currentOilRemainTime / 3;
            //15f;

            startIntensity = fireLight.intensity;
            startFireRate = new float[firech.Length];
            for (int i = 0; i < firech.Length; i++)
            {
                startFireRate[i] = firech[i].emission.rateOverTime.Evaluate(1f);
                //Debug.Log(startFireRate[i] + "" + firech.Length);
            }

        }
        void Update()
        {
            if (lantern.isSelected == true)
            {
                Debug.Log("lanterna selezionata");


                if (Input.GetKeyDown(KeyCode.Q))
                {


                    if (obj.ammo[0] >= 0 && currentOilRemainTime >= 0)
                    {
                        isOn = !isOn;
                        Debug.Log(isOn);

                    }
                    else
                        isOn = false;
                   
                        
                        

                    if (obj.getCurrentObj() == null)
                        return;

                    if (isOn)
                    {

                        currentOilRemainTime = ssr.GetRemainOil();
                        obj.ammo[0]--;
                            

                            fireLight.enabled = true;

                            fire.Play();
                            UI.UpdateResources("Matches", -1);
                        
                        
                    }
                    else
                    {
                        fireLight.enabled = false;
                        fire.Stop();
                        ssr.SetRemainOil(currentOilRemainTime);
                        if (obj.ammo[1] == 0)
                            Debug.Log("Olio finito");
                        if (obj.ammo[0] == 0)
                            Debug.Log("Fiammiferi finiti"); 
                    }


                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (obj.ammo[0] > 0 && obj.ammo[1] > 0)
                    {
                        Debug.Log("ricarica olio");
                        ssr.SetRemainOil(lantern.charge);
                        currentOilRemainTime = ssr.GetRemainOil();
                        obj.ammo[1]--;
                        UI.UpdateResources("Oil", -1);
                        UI.UpdateResources("Matches", -1);

                        // lanternLight
                        DOTween.Kill(fireLight);
                        fireLight.intensity = startIntensity;
                        for (int i = 0; i < firech.Length; i++)
                        {
                           var emission = firech[i].emission;
                           emission.rateOverTime = startFireRate[i];
                            //Debug.Log("Done");
                        }
                    } 
                }

                if (isOn)
                {


                    Debug.Log("decremento");
                    currentOilRemainTime -= decrementRate * Time.deltaTime;
                    //Debug.Log(Mathf.Round(currentTimeOfTorchLife));

                    //fadelight

                    if (currentOilRemainTime <= fireTimeLeftTot)
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
                    if (currentOilRemainTime <= 0)
                    {
                        isOn = false;
                        fireLight.enabled = false;
                        DOTween.Kill(fireLight);
                        fire.Stop();

                    }
                }
                


            }
            if (lantern.isSelected == false)
            {
                ssr.SetRemainOil(currentOilRemainTime);

            }

            UI.SetLLife(currentOilRemainTime);
        }
    }
}

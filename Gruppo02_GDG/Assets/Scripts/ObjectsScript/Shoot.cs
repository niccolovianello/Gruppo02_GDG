using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace Com.Kawaiisun.SimpleHostile
{
    public class Shoot : MonoBehaviour
    {
        public Camera cam;
        public GameObject arrowprefab;
        public Transform arrowSpawn;
        public float shootForce = 20f;
        private ObjectsManagement obj;
        private GameObject go;
        private Rigidbody rb;
        private Arrow arr;
        private BoxCollider bxcol;
        public Equipment bowEq;
        private PositionConstraint posCostraint;
        private AudioManager aud;
        private bool isAiming;
        public Equipment selectioBow;
        public GameObject arrowFake;
        public bool arrowFakeOn;
        public Light fireLight;
        public ParticleSystem fire;
        public float currentTimeOfMatchLife;
        public float decrementRate = 1f;
        public float maxTimeLife = 15f;
        //private ConstraintSource cosSource;

        public UIScript UI;

        private void Awake()
        {
            isAiming = false;
            UI = GameObject.Find("CanvasUI").GetComponent<UIScript>();
            if (UI == null)
                Debug.Log("not found UI from bow (shoot)");
            //Debug.Log(UI.name);
            isAiming = false;
        }


        private void Start()
        {
            obj = FindObjectOfType<ObjectsManagement>();
            cam = FindObjectOfType<Camera>();
            aud = FindObjectOfType<AudioManager>();
            arrowFake.SetActive(false);
            arrowFakeOn = false;
            currentTimeOfMatchLife = maxTimeLife;
            

        }

        void Update()
        {
            if (bowEq.isSelected == true)
            {
                if (Input.GetMouseButtonDown(1) && selectioBow.isSelected)

                {



                    isAiming = !isAiming;



                }
                if (!isAiming)
                    cam.GetComponent<MouseLook>().haveBow = false;
                if (isAiming)
                {
                    cam.GetComponent<MouseLook>().haveBow = true;

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        if (obj.ammo[3] > 0 && obj.ammo[0] > 0)
                        {
                            Debug.Log("freccia");

                            arrowFake.SetActive(true);
                            //if (arrowFakeOn == false)
                            //{
                            //    fireLight.enabled = false;
                            //    fire.Stop();
                            //}

                            //if (Input.GetKeyDown(KeyCode.Q))
                            //{
                            //    Debug.Log("premi q");

                                    //Debug.Log("freccia accesa");
                                    arrowFakeOn = true;
                                    aud.Play("Match");
                                    obj.ammo[0]--;
                                    fireLight.enabled = true;
                                    fire.Play();
                                    UI.UpdateResources("Matches", -1);
                              
                                
                            }
                            //go.transform.parent = arrowSpawn.transform;
                            //go.transform.localRotation = Quaternion.Euler(0, 0, 0);
                            //go.transform.localPosition = Vector3.zero;
                            
                           
                            //Quaternion.Euler(18.086f,191.95f,10.619f);
                            
                            
                            //go.transform.position = cam.transform.position + new Vector3(0, 0, 0.3f);
                            //posCostraint = go.GetComponent<PositionConstraint>();
                            
                            
                            //rb.isKinematic = false;
                            //rb.constraints = RigidbodyConstraints.FreezeAll;
                            //cosSource.sourceTransform = arrowSpawn.transform;
                            //cosSource.weight = 1;
                            //posCostraint.AddSource(cosSource);
                            
                        }
                        if (arrowFakeOn == true)
                        {
                            currentTimeOfMatchLife -= decrementRate * Time.deltaTime;

                            UI.SetALife(currentTimeOfMatchLife);

                            // fadelight

                            if (currentTimeOfMatchLife <= 0)
                            {
                                fireLight.enabled = false;
                                fire.Stop();
                                arrowFake.SetActive(false);
                                obj.ammo[3]--;
                            UI.UpdateResources("Arrows", -1);
                            currentTimeOfMatchLife = maxTimeLife;
                            }

                            //end fadelight
                        }


                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        if (arrowFake.activeSelf == true)
                        {
                            arrowFake.SetActive(false);

                            go = Instantiate(arrowprefab,arrowSpawn.position,Quaternion.identity) as GameObject;
                            rb = go.GetComponent<Rigidbody>();
                            bxcol = go.GetComponent<BoxCollider>();
                            arr = go.GetComponent<Arrow>();
                            if (go != null)
                                aud.Play("Arrow");
                            if (arr != null)
                                arr.isThrown = true;
                            if (bxcol == null)
                                return;

                            bxcol.enabled = true;
                            
                            rb.constraints = RigidbodyConstraints.None;
                            rb.useGravity = true;
                            rb.isKinematic = false;
                            rb.velocity = cam.transform.forward * shootForce;
                            obj.ammo[3]--;
                            UI.UpdateResources("Arrows", -1);
                        }


                    }

                



            }
            else
                cam.GetComponent<MouseLook>().haveBow = true;

        }

       
}
}
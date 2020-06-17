using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
       
        private AudioManager aud;
        private bool isAiming;
        public Equipment selectioBow;

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

                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("freccia");
                        go = Instantiate(arrowprefab, arrowSpawn.position, arrowSpawn.rotation, arrowSpawn) as GameObject;
                        go.transform.localRotation = Quaternion.Euler(0, 0, 0);
                        //Quaternion.Euler(18.086f,191.95f,10.619f);
                        Debug.Log(go.transform.localRotation);
                        arr = go.GetComponent<Arrow>();
                        //go.transform.position = cam.transform.position + new Vector3(0, 0, 0.3f);

                        rb = go.GetComponent<Rigidbody>();
                        bxcol = go.GetComponent<BoxCollider>();
                        bxcol.enabled = false;
                        //rb.isKinematic = false;
                        rb.constraints = RigidbodyConstraints.FreezeAll;

                        UI.UpdateResources("Arrows", -1);
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        aud.Play("Arrow");
                        arr.isThrown = true;
                        if (bxcol == null)
                            return;
                        bxcol.enabled = true;
                        rb.constraints = RigidbodyConstraints.None;
                        rb.velocity = cam.transform.forward * shootForce;
                        obj.ammo[3]--;


                    }
                }



            }
            else
                cam.GetComponent<MouseLook>().haveBow = true;

        }

       
}
}
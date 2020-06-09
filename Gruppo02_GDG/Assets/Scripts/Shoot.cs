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
        

        private void Start()
        {
            obj = FindObjectOfType<ObjectsManagement>();
            cam = FindObjectOfType<Camera>();
        }

        void Update()
        {

            if (Input.GetMouseButton(1))

            {
                cam.GetComponent<MouseLook>().haveBow = true;

                if (Input.GetMouseButtonDown(0))
                {

                    go = Instantiate(arrowprefab, arrowSpawn.position,arrowSpawn.localRotation);
                    arr = go.GetComponent<Arrow>();
                    go.transform.position = cam.transform.position + new Vector3(0, 0, 0.3f);
                    rb = go.GetComponent<Rigidbody>();
                    //rb.isKinematic = false;
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                }

            }
            else
                cam.GetComponent<MouseLook>().haveBow = false;
            


           
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButton(1))

            {
                if (Input.GetMouseButtonDown(0))
                {

                   
                   

                }

                if (Input.GetMouseButtonUp(0))


                {

                    arr.isThrown = true;

                    rb.constraints = RigidbodyConstraints.None;
                    rb.velocity = cam.transform.forward * shootForce;
                    obj.ammo[3]--;


                }
            }
        }
}
}
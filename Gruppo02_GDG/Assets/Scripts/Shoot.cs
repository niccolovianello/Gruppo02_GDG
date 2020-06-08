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
            if (Input.GetMouseButtonDown(0) && Input.GetMouseButton(1))
            {
                
                GameObject go = Instantiate(arrowprefab, arrowSpawn.position, Quaternion.identity);
                arr = go.GetComponent<Arrow>();
                go.transform.localEulerAngles = transform.forward;
                rb = go.GetComponent<Rigidbody>();
                //rb.isKinematic = false;
                rb.constraints = RigidbodyConstraints.FreezeAll;

            }
            
            if ((Input.GetMouseButtonUp(0) && Input.GetMouseButton(1)))
            {
                //if (obj.ammo[3] > 0)
                //{

                arr.isThrown = true;
                //rb.isKinematic = true;
                rb.constraints = RigidbodyConstraints.None;
                rb.velocity = cam.transform.forward * shootForce;
                obj.ammo[3]--;
                //}
                //else
                //{
                //    Debug.Log("No more Arrows");
                //}
              
            }
            //if (go != null && !Input.GetMouseButton(1))
            //{
            //    Destroy(go);
            //}
        }

        private void FixedUpdate()
        {
            if (rb != null)
            {
                
            }
        }
    }
}
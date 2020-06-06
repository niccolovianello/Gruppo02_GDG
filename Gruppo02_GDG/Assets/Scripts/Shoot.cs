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
        

        private void Start()
        {
            obj = FindObjectOfType<ObjectsManagement>();
            cam = FindObjectOfType<Camera>();
        }

        void Update()
        {
            //Debug.Log(obj);
            if (Input.GetMouseButtonDown(0))
            {
                //if (obj.ammo[3] > 0)
                //{
                    GameObject go = Instantiate(arrowprefab, arrowSpawn.position, Quaternion.Euler(90,0,0));
                    go.transform.localEulerAngles = transform.forward;
                    Rigidbody rb = go.GetComponent<Rigidbody>();
                    rb.velocity = cam.transform.forward * shootForce;
                    obj.ammo[3]--;
                //}
                //else
                //{
                //    Debug.Log("No more Arrows");
                //}
              
            }
        }
    }
}
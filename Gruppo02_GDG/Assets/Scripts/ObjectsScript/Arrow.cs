using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Kawaiisun.SimpleHostile
{
    public class Arrow : MonoBehaviour
    {
        public bool isOn;
        public ParticleSystem fire;
        private ParticleSystem[] firech;
        public Light fireLight;
        public float currentTimeOfMatchLife;
        public float decrementRate = 1f;      
       
       

        Rigidbody arrowBody;
        private float lifeTimer = 2f;
        private float timer;
        private bool hitSomething = false;
        public bool isThrown = false;
        private AudioManager aud;

        public UIScript UI;

        private void Awake()
        {
            UI = GameObject.Find("CanvasUI").GetComponent<UIScript>();
            if (UI == null)
                Debug.Log("not found UI from arrow");
            //Debug.Log(UI.name);
        }

        void Start()
        {
            aud = FindObjectOfType<AudioManager>();
            //obj= FindObjectOfType<ObjectsManagement>();
            //currentTimeOfMatchLife = maxTimeLife;
            isOn = false;
           
            arrowBody = GetComponent<Rigidbody>();
            transform.rotation = Quaternion.LookRotation(arrowBody.velocity);
            

        }


        void Update()
        {
            
          
            if (isThrown)
            {
                //arrowBody.isKinematic = false;
                //arrowBody.useGravity = true;
                timer += Time.deltaTime;

                UI.SetALife(0);

                if (timer >= lifeTimer)
                {
                    Destroy(gameObject);
                }

                if (!hitSomething)
                    transform.rotation = Quaternion.LookRotation(arrowBody.velocity);
            }

            //UI.SetALife(currentTimeOfMatchLife);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag != "Arrow")
            {
                hitSomething = true;
                Stick();
            }

        }

        private void Stick()
        {
            arrowBody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
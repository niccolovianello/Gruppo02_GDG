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
        public float maxTimeLife = 15f;
        private ObjectsManagement obj;

        Rigidbody arrowBody;
        private float lifeTimer = 2f;
        private float timer;
        private bool hitSomething = false;
        public bool isThrown = false;
        private AudioManager aud;

        void Start()
        {
            aud = FindObjectOfType<AudioManager>();
            obj= FindObjectOfType<ObjectsManagement>();
            currentTimeOfMatchLife = maxTimeLife;
            isOn = false;
            fire.Stop();
            fireLight.enabled = false;
            arrowBody = GetComponent<Rigidbody>();
            transform.rotation = Quaternion.LookRotation(arrowBody.velocity);
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (obj.ammo[0] > 0)
                {
                    aud.Play("Match");
                    obj.ammo[0]--;
                    Debug.Log(obj.ammo[0]);
                    isOn = !isOn;
                    if (isOn)
                    {
                        fireLight.enabled = true;
                        fire.Play();
                    }
                }
                else
                    isOn = false;


            }
            if (isOn)
            {
                currentTimeOfMatchLife -= decrementRate * Time.deltaTime;

                // fadelight

                if (currentTimeOfMatchLife <= 0)
                {

                    Destroy(this.gameObject);
                }

                //end fadelight
            }
            if (isThrown)
            {
                timer += Time.deltaTime;

                if (timer >= lifeTimer)
                {
                    Destroy(gameObject);
                }

                if (!hitSomething)
                    transform.rotation = Quaternion.LookRotation(arrowBody.velocity);
            }

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
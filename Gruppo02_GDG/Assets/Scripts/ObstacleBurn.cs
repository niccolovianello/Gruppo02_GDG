using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Kawaiisun.SimpleHostile
{
    public class ObstacleBurn : MonoBehaviour
    {
        public ParticleSystem fire;
        public Light fireLight;
        public float timeToBurn = 2;

        void Start()
        {
            fire.Stop();
            fireLight.enabled = false;
        }

        private void onCollisionEnter(Collision collision)
        {
            string tag = collision.collider.tag;
            if (tag == "Object")
            {
                Debug.Log("object");
                if (collision.collider.GetComponent<TorchOnOff>().isOn == true)
                {
                    Debug.Log("brucia");
                    StartCoroutine(Burn(timeToBurn));
                    fireLight.enabled = true;
                    fire.Play();
                }
                    
            }
        }

        IEnumerator  Burn(float t)
        {
            yield return new WaitForSeconds(t);
            Destroy(this);
        }
    

    }
}
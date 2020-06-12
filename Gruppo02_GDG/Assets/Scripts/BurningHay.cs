using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Kawaiisun.SimpleHostile
{
    public class BurningHay : MonoBehaviour
    {
        public ParticleSystem fire;
        //public Light fireLight;
        public float timeBurning;

        void Start()
        {
            fire.Stop();
        }

        private void OnTriggerEnter(Collider other)
        {
            foreach (Transform child in other.gameObject.transform.GetComponentsInChildren<Transform>())
            {
                if(child.tag.Equals("Object") == true)
                {
                    if (child.gameObject.GetComponent<TorchOnOff>().isOn == true)
                    {
                        StartCoroutine(Burn(timeBurning));
                        fire.Play();
                    }
                }
            }
        }

        IEnumerator Burn(float t)
        {
            yield return new WaitForSeconds(t);
            Destroy(this.gameObject);
        }
    }
}

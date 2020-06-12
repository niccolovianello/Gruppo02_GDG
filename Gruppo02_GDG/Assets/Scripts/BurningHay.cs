using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Kawaiisun.SimpleHostile
{
    public class BurningHay : MonoBehaviour
    {
        public ParticleSystem fire;
        //public Light fireLight;
        public float timeBurning = 4f;

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
                    Debug.Log("equals");
                    if (child.gameObject.GetComponent<TorchOnOff>().isOn == true)
                    {
                        Debug.Log("brucia");
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

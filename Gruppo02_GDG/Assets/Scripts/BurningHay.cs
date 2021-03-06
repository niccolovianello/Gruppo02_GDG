﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Kawaiisun.SimpleHostile
{
    public class BurningHay : MonoBehaviour
    {
        public ParticleSystem fire;
        //public Light fireLight;
        public float timeBurning;
        private AudioManager aud;

        void Start()
        {
            fire.Stop();
            aud = FindObjectOfType<AudioManager>();
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
                        aud.Play("Paglia");
                    }
                    
                }
            }

            if (other.gameObject.GetComponent<Arrow>())
            {
                StartCoroutine(Burn(timeBurning));
                fire.Play();
                aud.Play("Paglia");
            }
        }

        IEnumerator Burn(float t)
        {
            yield return new WaitForSeconds(t);
            aud.Stop("Paglia");
            Destroy(this.gameObject);
        }
    }
}

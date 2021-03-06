﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Kawaiisun.SimpleHostile
{
    
    public class PlayerPickUpResources : MonoBehaviour
    {
        public int Oil = 1;
        public int Matches = 5;
        public int Batteries = 1;
        public int Arrows = 1;
        public ObjectsManagement obj;
        private AudioManager aud;


        public UIScript UI;

        private void Start()
        {
            aud = FindObjectOfType<AudioManager>();
        }
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.tag == "Arrows")
            {
                aud.Play("PickResource");
                obj.ammo[3] = obj.ammo[3] + Arrows;
                hit.collider.enabled = false;
                Destroy(hit.gameObject);

                UI.UpdateResources(hit.gameObject.tag, Arrows);
            }
            if (hit.gameObject.tag == "Battery")
            {
                aud.Play("PickResource");
                Debug.Log(hit.gameObject.name);
                obj.ammo[2] = obj.ammo[2] + Batteries;
                hit.collider.enabled = false;
                Destroy(hit.gameObject);

                UI.UpdateResources(hit.gameObject.tag, Batteries);
            }

            if (hit.gameObject.tag == "Oil")
            {
                BoxCollider h = hit.gameObject.GetComponent<BoxCollider>();
                h.enabled = false;
                aud.Play("PickResource");
                obj.ammo[1] = obj.ammo[1] + Oil;
                hit.collider.enabled = false;
                Debug.Log(hit.gameObject.name);
                Destroy(hit.gameObject);

                UI.UpdateResources(hit.gameObject.tag, Oil);
            }
            if (hit.gameObject.tag == "Matches")
            {
                aud.Play("PickResource");
                obj.ammo[0] = obj.ammo[0] + Matches;
                hit.collider.enabled = false;
                Destroy(hit.gameObject);

                UI.UpdateResources(hit.gameObject.tag, Matches);
            }
            if (hit.gameObject.tag == "CurativeObject")
            {
                aud.Play("PickResource");
                obj.ammo[4] = obj.ammo[4] + 1;
                hit.collider.enabled = false;
                Destroy(hit.gameObject);

                UI.UpdateResources(hit.gameObject.tag, 1);
            }


        }

    }

}


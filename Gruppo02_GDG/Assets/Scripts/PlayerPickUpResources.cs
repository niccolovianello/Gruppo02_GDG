using System.Collections;
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
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.name == "Arrows")
            {
                obj.ammo[3] = obj.ammo[3] + Arrows;
                hit.collider.enabled = false;
                Destroy(hit.gameObject);
            }
            if (hit.gameObject.name == "Battery")
            {
                Debug.Log(hit.gameObject.name);
                obj.ammo[2] = obj.ammo[2] + Batteries;
                hit.collider.enabled = false;
                Destroy(hit.gameObject);
            }

            if (hit.gameObject.name == "Oil")
            {
                obj.ammo[1] = obj.ammo[1] + Oil;
                hit.collider.enabled = false;
                Destroy(hit.gameObject);
            }
            if (hit.gameObject.name == "Matches")
            {
                obj.ammo[0] = obj.ammo[0] + Matches;
                hit.collider.enabled = false;
                Destroy(hit.gameObject);
            }
           
        }

    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Kawaiisun.SimpleHostile
{
    public class PickObjects : MonoBehaviour
    {

        public ObjectsManagement obj;

        public PickObUI pickUI;
      

        void Update()
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 4, Color.yellow);

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 4))
            {
                if (hit.collider.gameObject.tag == "Equipment")
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log(hit.collider.gameObject.name);
                        obj.PickEquipment(hit.collider.gameObject.name);
                        Destroy(hit.collider.gameObject);
                    }

                    pickUI.DotEnlight();
                }
            }
            else
            {
                pickUI.DotNormal();
            }
        }


    }

}

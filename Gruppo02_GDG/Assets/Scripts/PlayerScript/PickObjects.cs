using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Kawaiisun.SimpleHostile
{
    public class PickObjects : MonoBehaviour
    {

        public ObjectsManagement obj;
        private SupportScriptResources ssr;
        public PickObUI pickUI;
        private AudioManager aud;


        private void Start()
        {
            aud = FindObjectOfType<AudioManager>();
            ssr = FindObjectOfType<SupportScriptResources>();
        }
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
                       

                        if (obj.PickEquipment(hit.collider.gameObject.name) == true)
                        {
                            aud.Play("ReloadFlashlight");
                            Destroy(hit.collider.gameObject);
                            Debug.Log(hit.collider.gameObject);
                        }
                        //if (obj.PickEquipment(hit.collider.gameObject.name) == false)
                        //{
                        //    if (hit.collider.gameObject.name == "Torch")
                        //    {
                        //        Debug.Log(hit);
                        //        ssr.SetRemainLifeTorch(50f);
                        //        aud.Play("ReloadFlashlight");
                        //        Destroy(hit.collider.gameObject);
                        //        return;

                        //    }
                        //}
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

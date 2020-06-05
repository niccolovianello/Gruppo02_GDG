using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Kawaiisun.SimpleHostile
{
    public class UISlot : MonoBehaviour
    {
        bool selected;
        string name;
        string letterswitch;

        float batterylife;
        float torchlife;
        float matchlife;
        float lanternlife;

        Image charge;
        //float bowlife; //POI AGGIUNGI

        // Start is called before the first frame update
        void Start()
        {
            charge = this.transform.Find("Charge").GetComponent<Image>();

            charge.fillAmount = 0;
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(batterylife);
            Debug.Log(torchlife);
            Debug.Log(matchlife);
            Debug.Log(lanternlife);

            PickBackground();
            if (selected == true)
            {
                //if (letterswitch != name)
                    letterswitch = name;
                PickName();
                DoFade();
                //Debug.Log(name);
            }
        }

        void PickName()
        {
            if (this.transform.Find("WeaponName"))
            {
                //Debug.Log("found" + this.transform.Find("WeaponName").GetComponent<Text>().text);
                if (this.transform.Find("WeaponName").GetComponent<Text>().text != "")
                {
                    var s = this.transform.Find("WeaponName").GetComponent<Text>().text;
                    name = s.Substring(0,1);
                }
            }
            else
            {
                //Debug.Log("found matches" + this.transform.Find("ResourceName").GetComponent<Text>().text);
                //name = this.transform.Find("ResourceName").GetComponent<Text>().text;
                name = "M";
            }
        }

        void PickBackground()
        {
            Color32 disabled = new Color32(100, 100, 100, 100);
            Color32 enabled = new Color32(100, 100, 100, 255);

            if (this.GetComponent<Image>().color == enabled)
            {
                selected = true;
            }
            else if (this.GetComponent<Image>().color == disabled)
            {
                selected = false;
            }
        }

        void DoFade()
        {
            Debug.Log("entra con " + letterswitch);

            switch (letterswitch)
            {
                case "M":
                    charge.fillAmount = (matchlife / 15);
                    Debug.Log(matchlife / 15 + "match");
                    break;
                case "T":
                    charge.fillAmount = (torchlife / 50);
                    Debug.Log(torchlife / 50 + "torch");
                    break;
                case "F":
                    charge.fillAmount = (batterylife / 50);
                    Debug.Log(batterylife / 50 + "battery");
                    break;
                case "L":
                    charge.fillAmount = (lanternlife / 50);
                    Debug.Log(lanternlife / 50 + "lantern");
                    break;
                //case "B":
                //    
                //    break;
                default:
                    print("Incorrect weapon");
                    charge.fillAmount = 0;
                    break;
            }

            //if(letterswitch == "M")
            //{
            //    charge.fillAmount = (matchlife / 15);
            //    Debug.Log(matchlife / 15 + "match");
            //}
            //else if(letterswitch == "T")
            //{
            //    charge.fillAmount = (torchlife / 50);
            //    Debug.Log(torchlife / 50 + "torch");
            //}
            //else if (letterswitch == "F")
            //{
            //    charge.fillAmount = (batterylife / 50);
            //    Debug.Log(batterylife / 50 + "battery");
            //}
            //else if (letterswitch == "L")
            //{
            //    charge.fillAmount = (lanternlife / 50);
            //    Debug.Log(lanternlife / 50 + "lantern");
            //}
            ////else if (letterswitch == "B")
            ////{

            ////}
            //else
            //{
            //    Debug.Log("Incorrect weapon");
            //    charge.fillAmount = 0;
            //}
        }

        public void SetFBattery(float currentbattery)
        {
            batterylife = currentbattery;
        }

        public void SetTLife(float currentTlife)
        {
            torchlife = currentTlife;
        }

        public void SetMLife(float currentMlife)
        {
            matchlife = currentMlife;
        }

        public void SetLLife(float currentLlife)
        {
            lanternlife = currentLlife;
        }
    }
}

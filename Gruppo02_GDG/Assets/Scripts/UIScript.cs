using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Com.Kawaiisun.SimpleHostile
{
    public class UIScript : MonoBehaviour
    {
        Transform Weapons;
        Transform Resources;
        Transform InfoPanel;
        Transform DeathPanel;
        //Text t = child.GetComponent<Text>();

        // Start is called before the first frame update
        void Start()
        {
            Resources = transform.Find("Resources");
            Weapons = transform.Find("WeaponsInventory");
            InfoPanel = transform.Find("InfoPanel");
            DeathPanel = transform.Find("DeathPanel");
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void UpdateWeapons(string weaponname, int weaponpos)
        {
            Transform weapSlot = Weapons.Find("Panel/WeaponSlot(" + weaponpos + ")");
            weapSlot.GetComponentInChildren<Text>().text = weaponname;
        }

        public void ActiveWeapon(int oldwpos, int newwpos)
        {
            Weapons.Find("Panel/WeaponSlot(" + oldwpos + ")").GetComponent<Image>().color = new Color32(100,100,100,100);
            Weapons.Find("Panel/WeaponSlot(" + newwpos + ")").GetComponent<Image>().color = new Color32(100, 100, 100, 255);
        }

        public void UpdateResources(string resourcename, int resourcenumber)
        {
            Transform resSlot = Resources.Find("Panel/" + resourcename);
            Text numRes = resSlot.Find("ResourceNumberCircle").GetComponentInChildren<Text>();
            int oldRes = int.Parse(numRes.text);
            int tot = oldRes + resourcenumber;
            numRes.text = tot.ToString();

            Text addrem = resSlot.Find("AddRemove").GetComponentInChildren<Text>();
            if (resourcenumber > 0)
            {
                addrem.color = new Color32(0, 255, 0, 255);
                addrem.text = "+" + resourcenumber;
            }
            else
            {
                addrem.color = new Color32(255, 0, 0, 255);
                addrem.text = "-" + resourcenumber;
            }

            StartCoroutine(ExecuteAfterTime(0.5f, addrem));
        }

        public void UpdateInfo(string info)
        {
            Text infotext = InfoPanel.GetComponentInChildren<Text>();
            infotext.text = info;

            //StartCoroutine(InfoAfterTime(2f, infotext));
        }

        public void HurtUI(float damage)
        {
            //DeathPanel.GetComponent<Image>().DOColor();
        }

        public void TimerDarkUI(float timer)
        {
            //DeathPanel.GetComponent<Image>().DOColor();
        }

        /*public void UnSeenPlayerUI(bool seen)
        {
            if(seen == true)
            {
                transform.GetComponentInChildren<>
            }
        }*/

        IEnumerator ExecuteAfterTime(float time, Text t)
        {
            yield return new WaitForSeconds(time);

            t.color = new Color32(0, 0, 0, 0);
        }

        /*IEnumerator InfoAfterTime(float time, Text t)
        {
            yield return new WaitForSeconds(time);

            t.text = "";
        }*/
    }
}

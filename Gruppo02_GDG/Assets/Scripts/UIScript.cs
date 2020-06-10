using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Runtime.CompilerServices;

namespace Com.Kawaiisun.SimpleHostile
{
    public class UIScript : MonoBehaviour
    {
        Transform Weapons;
        Transform Resources;
        Transform MapPanel;
        Transform DeathPanel;

        float alpha;
        float alpha1;

        float batterylife;
        float torchlife;
        float matchlife;
        float lanternlife;

        bool isActive;
        //Text t = child.GetComponent<Text>();

        // Start is called before the first frame update
        void Awake()
        {
            Resources = transform.Find("Resources");
            Weapons = transform.Find("WeaponsInventory");
            MapPanel = transform.Find("MapPanel");
            DeathPanel = transform.Find("DeathPanel");

            isActive = false;
        }

        //// Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                isActive = !isActive;

                OpenMap(isActive);
            }
        }

        public void UpdateWeapons(string weaponname, int weaponpos)
        {
            int pos = weaponpos + 1;
            Transform weapSlot = Weapons.Find("Panel/WeaponSlot (" + pos + ")");
            weapSlot.GetComponentInChildren<Text>().text = weaponname;
        }

        public void ActiveWeapon(int newwpos)
        {
            //Debug.Log(newwpos);
            Image Matches = Resources.Find("Panel/Matches").GetComponent<Image>();
            if (newwpos != 4)
            {
                for (int i = -1; i < 3; i++)
                {
                    if (newwpos == i)
                    {
                        if (newwpos == -1)
                        {
                            Matches.color = new Color32(100, 100, 100, 255);
                        }
                        else
                        {
                            Weapons.Find("Panel/WeaponSlot (" + (newwpos + 1) + ")").GetComponent<Image>().color = new Color32(100, 100, 100, 255);
                        }
                    }
                    else
                    {
                        if (i == -1)
                        {
                            Matches.color = new Color32(100, 100, 100, 100);
                        }
                        else
                        {
                            Weapons.Find("Panel/WeaponSlot (" + (i + 1) + ")").GetComponent<Image>().color = new Color32(100, 100, 100, 100);
                        }
                    }
                }
            }
            else
            {
                for (int i = -1; i < 3; i++)
                {
                    if(i == -1)
                    {
                        Matches.color = new Color32(100, 100, 100, 100);
                    }
                    else
                    {
                        Weapons.Find("Panel/WeaponSlot (" + (i + 1) + ")").GetComponent<Image>().color = new Color32(100, 100, 100, 100);
                    }
                }
            }
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

        public void OpenMap(bool open)
        {
            if (open)
            {
                MapPanel.DOScale(5, 0.5f);
                return;
            }
            else
            {
                MapPanel.DOScale(1, 0.5f);
                return;
            }
        }

        public void HurtUI(float damage)
        {
            if (damage >= 255)
            {
                alpha = damage;
                DeathPanel.GetComponentInChildren<Text>().DOColor(new Color32(255, 255, 255, 255), 0.7f);
            }
            else if (damage == 0)
            {
                alpha = damage;
            }
                
            else
            {
                alpha += (damage * 0.5f);
            }
            DeathPanel.GetComponent<Image>().DOColor(new Color32(138, 3, 3, (byte)alpha), 0.5f);
        }

        public void TimerDarkUI(float timer)
        {
            if (timer >= 255)
            {
                alpha1 = timer;
                DeathPanel.GetComponentInChildren<Text>().DOColor(new Color32(255, 255, 255, 255), 0.7f);
            }
            else if (timer >= 30f)
            {
                alpha1 = 0f;
            }
            else
            {
                alpha1 = (255 / (timer/4)); //timer/n, where n++, smoother is darkness
            }
            DeathPanel.GetComponent<Image>().DOColor(new Color32(0, 0, 0, (byte)alpha1), 0.5f);

            //Debug.Log(alpha1 + " " + timer);
        }

        IEnumerator ExecuteAfterTime(float time, Text t)
        {
            yield return new WaitForSeconds(time);

            t.color = new Color32(0, 0, 0, 0);
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

        // ADD BOW

        public float GetFBattery()
        {
            return batterylife;
        }

        public float GetTLife()
        {
            return torchlife;
        }

        public float GetMLife()
        {
            return matchlife;
        }

        public float GetLLife()
        {
            return lanternlife;
        }

        // ADD BOW
    }
}

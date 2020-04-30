using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Kawaiisun.SimpleHostile
{
    public class ObjectsManagement : MonoBehaviour
    {
        public Equipment[] loadout;
        public Equipment[] pickLoadout;
        public Transform objectParent;
        private GameObject currentObject;
        public PlayerCombatScript weaponProperties;

        private int currentIndex = 0;
        

        private void Start()
        {
            pickLoadout = new Equipment[3];
        }

        void Update()

        {
            //if (Input.GetKeyDown(KeyCode.Tab))
            //{ if (currentIndex == 2)
            //    {
            //        Equip(0);
            //        currentIndex = 0;
                    
            //    }
            //    else {
            //        Equip(currentIndex + 1);
            //        currentIndex++;
            //    }
                
            //}

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Equip(0);
                currentIndex = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Equip(1);
                currentIndex = 1;
            } 

            if (currentObject != null)
            {
                Aim(Input.GetMouseButton(1));
            }


        }


        void Equip(int eq_index)

        {

            if (currentObject != null)
            {
                Destroy(currentObject);
                pickLoadout[currentIndex].isSelected = false;
            } 

            GameObject t_newEquipment = Instantiate(pickLoadout[eq_index].prefab, objectParent.position, objectParent.rotation, objectParent) as GameObject;
            
            t_newEquipment.transform.localPosition = Vector3.zero;
            t_newEquipment.transform.localEulerAngles = Vector3.zero;

            weaponProperties.attackRange = pickLoadout[eq_index].attackRange;
            weaponProperties.attackRate = pickLoadout[eq_index].attackRate;
            weaponProperties.attackDamage = pickLoadout[eq_index].damage;
           

            currentObject = t_newEquipment;
            currentIndex = eq_index;
            pickLoadout[eq_index].isSelected = true;

        }

        void Aim(bool isAiming)
        {
            if (currentObject == null)
                return;

            Transform t_anchor = currentObject.transform.Find("Anchor");
            Transform t_state_ads = currentObject.transform.Find("States/ADS");
            Transform t_state_hip = currentObject.transform.Find("States/Hip");

            if (isAiming)
            {
                t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_ads.position, Time.deltaTime * pickLoadout[currentIndex].aimSpeed);
            }
            else
            {

                t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_hip.position, Time.deltaTime * pickLoadout[currentIndex].aimSpeed);
            }

        }

        public void PickEquipment(string equipmentPick)
        { 
           
            int index=0;
            int indexCounting = 0;
            int ind_pick= 3000;
            bool replacement = true;
            bool isPlaced = false;

            foreach (Equipment e in loadout)
            {
                if (e.name == equipmentPick)
                {
                    index = indexCounting;
                    Debug.Log("E' entrato");
                }
                else
                    indexCounting++;
            }

            for (int i = 0; i < 3; i++)
            {
                if (pickLoadout[i] == null && isPlaced == false)
                {
                    pickLoadout[i] = loadout[index];
                    ind_pick = i;
                    replacement = false;
                    isPlaced = true;
                }
               
            }
            if (replacement == true)
            {
                pickLoadout[currentIndex] = loadout[index];
                ind_pick = currentIndex;

            }

            Debug.Log(ind_pick);

            Equip(ind_pick);
        }

    }
}
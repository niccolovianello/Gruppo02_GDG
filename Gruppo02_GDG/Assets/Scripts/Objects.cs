using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Kawaiisun.SimpleHostile { 
public class Objects : MonoBehaviour
{
    public Equipment[] loadout;
    public Transform objectParent;
    private int currentIndex;

    private GameObject currentObject;
  

 
    
        void Update()
    
        {
        
            if (Input.GetKeyDown(KeyCode.Alpha1)) Equip(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) Equip(1);

            if (currentObject != null)
            {
                Aim(Input.GetMouseButton(1));
            }
            
    
        }

    
        void Equip(int eq_index)
    
        {
           

                
                
                if (currentObject != null) Destroy(currentObject);
               
                

                GameObject t_newEquipment = Instantiate(loadout[eq_index].prefab, objectParent.position, objectParent.rotation, objectParent) as GameObject;

                t_newEquipment.transform.localPosition = Vector3.zero;

                t_newEquipment.transform.localEulerAngles = Vector3.zero;


                currentObject = t_newEquipment;

                

                currentIndex = eq_index;

                    
        
            

            
    
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
                t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_ads.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
            }
            else {

                t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_hip.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
            }

        }

}
}
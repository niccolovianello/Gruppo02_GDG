using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Animations;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine;

namespace Com.Kawaiisun.SimpleHostile
{
    public class PlayerCombatScript : MonoBehaviour
    {
        public bool isOn = false;
        public LayerMask enemyLayers;
        public Camera fpsCam;
        EnemyGeneral en;
        public Animator animationObj;


        public float attackRange = 50f;
        public int attackDamage = 40;

        public float attackRate = 1f;
        float nextAttackTime = 0f;

        private EnemyGeneral enem;
        


      
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Time.time >= nextAttackTime)
                {
                    Attack();
                    nextAttackTime = Time.time + 1 / attackRate;
                }


            }
        }

        public void Attack()
        {
            if (attackRange == 3)
            {
                
                    animationObj.SetTrigger("TorchAttack");

            }
            else if (attackRange == 15)
            {
                RaycastHit hit;
                if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, attackRange))
                {
                    
                    if (hit.collider.GetComponent<EnemyGeneral>() && isOn == true )
                    {

                        //animationObj.SetTrigger("TorchAttack");
                            Debug.Log("colpisco distante");
                            enem = hit.collider.GetComponent<EnemyGeneral>();
                            enem.TakeDamage(attackDamage);
                            enem.FlamePart();
                        
                    }
                }
            }
        }



    }
}

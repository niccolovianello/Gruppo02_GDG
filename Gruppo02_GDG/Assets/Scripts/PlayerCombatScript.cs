﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Animations;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine;

public class PlayerCombatScript : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public Camera fpsCam;
    EnemyGeneral en;
   public Animator animationObj;
    

    public float attackRange =50f;
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
        if (attackRange < 3)
        {
            animationObj.SetTrigger("TorchAttack");
            
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                Debug.Log("colpisco vicino");
                enem = enemy.GetComponent<EnemyGeneral>();
                //animationObj.SetTrigger("TorchAttack");

                if (enem != null)
                {
                    enem.TakeDamage(attackDamage);
                    enem.HurtPart();
                }
               
            }
            
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, attackRange))
            {
                if (hit.collider.GetComponent<EnemyGeneral>())
                {               
                    Debug.Log("colpisco distante");
                    enem = hit.collider.GetComponent<EnemyGeneral>();
                    enem.TakeDamage(attackDamage);
                    enem.FlamePart();
                }
            }
        }
    }
       

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void ActivateHitTorchk()
        {
            this.GetComponent<SphereCollider>().enabled = true;
        }

    public void DeactivateHitTorch()
        {
            this.GetComponent<SphereCollider>().enabled = false;
        }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatScript : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public Camera fpsCam;
    EnemyGeneral en;
    

    public float attackRange =50f;
    public int attackDamage = 40;

    public float attackRate = 1f;
    float nextAttackTime = 0f;

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
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                Debug.Log("colpisco vicino");
                enemy.GetComponent<EnemyGeneral>().TakeDamage(attackDamage);
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
                    hit.collider.GetComponent<EnemyGeneral>().TakeDamage(attackDamage);
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
}

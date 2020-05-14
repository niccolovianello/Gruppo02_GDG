using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("il nemico ti ha colpito");

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MonsterFist")
        {
            TakeDamage(30);
        }
    }
    void Die()
    {
        
        Debug.Log("Player die");
    }
}

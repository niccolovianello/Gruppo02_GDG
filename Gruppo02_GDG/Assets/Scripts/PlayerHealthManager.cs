using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public float impactMonster = 500;
    public float ratioHealth = 3f;

    void Start()
    {
        currentHealth = maxHealth;

    }

    private void Update()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += ratioHealth * Time.deltaTime;
        }
        
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
        SceneManager.LoadScene("SampleScene");
    }
}

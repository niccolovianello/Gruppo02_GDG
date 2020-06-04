﻿using Com.Kawaiisun.SimpleHostile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneral : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public ParticleSystem flames;
    public ParticleSystem hurt;

    public PatrolPointsData ppd;

    public EnemyController en;

    private void Awake()
    {
        hurt.Stop();
        flames.Stop();

        en = FindObjectOfType<EnemyController>();

        ppd = FindObjectOfType<PatrolPointsData>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(en.GetChasing() == false && en.GetFollowing() == false) // TO FIX
        {
            en.SetSeen();
        }

        if (currentHealth <= 0)
        {
            Die();
            ppd.SetDeadEnemies();
            Respawn();
        }

    }

    void Die()
    {
        Destroy(this.gameObject);
        Debug.Log("Enemy die");
    }

    public void HurtPart()
    {
        hurt.Play();
    }

    public void FlamePart()
    {
        flames.Play();
    }

    public void Respawn()
    {
        ppd.RespawnEnemy();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerAttackCloseRange")
        {
            TakeDamage(40);
            Debug.Log("hit enemy");
            HurtPart();
        }
    }
}

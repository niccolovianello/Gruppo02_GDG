using Com.Kawaiisun.SimpleHostile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGeneral : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public ParticleSystem flames;
    public ParticleSystem hurt;

    public PatrolPointsData ppd;

    public EnemyController en;

    private AudioManager aud;

    public PlayerLife plife;

    private void Awake()
    {
        aud = FindObjectOfType<AudioManager>();
        hurt.Stop();
        flames.Stop();

        //en = FindObjectOfType<EnemyController>();

        en = this.GetComponent<EnemyController>();

        ppd = FindObjectOfType<PatrolPointsData>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(en.GetChasing() == false && en.GetFollowing() == false)
        {
            en.SetSeen();
        }

        en.SetHurt();

        if (currentHealth <= 0)
        {
            Die();
            ppd.SetDeadEnemies();
            Respawn();
        }

    }

    void Die()
    {
        aud.Play("MonsterDead");
        en.SetDie();

        this.GetComponent<NavMeshAgent>().baseOffset = -1;

        Destroy(this.gameObject, 5f);
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

        if (other.gameObject.tag == "Arrow")
        {
            Die();
        }
    }
}

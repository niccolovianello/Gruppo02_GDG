using Com.Kawaiisun.SimpleHostile;
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
    private ObjectsManagement obj;
    public AudioClip[] steps;
    private AudioSource audioSource;
    private AudioManager aud;
    public int damageMonster = 26;

    public UIScript UI;

    void Start()
    {
        aud = FindObjectOfType<AudioManager>();
        audioSource = FindObjectOfType<AudioSource>();
        currentHealth = maxHealth;
        obj = FindObjectOfType<ObjectsManagement>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            if (obj.ammo[4] > 0)
            {
                aud.Play("Cure");
                currentHealth = maxHealth;
                obj.ammo[4]--;
                UI.HurtUI(0);
                UI.UpdateResources("CurativeObject", -1);
                Debug.Log("cura");
            }
            

    }
    private AudioClip GetRandomClip()
    {
        return steps[UnityEngine.Random.Range(0, steps.Length)];
    }


    public void TakeDamage(int damage)
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
        currentHealth -= damage;
        Debug.Log("il nemico ti ha colpito");

        UI.HurtUI(damageMonster);

        if (currentHealth <= 0)
        {
            Die();

            UI.HurtUI(255);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MonsterFist")
        {
            TakeDamage(damageMonster);
            Debug.Log("hit player");
        }
    }
    void Die()
    {
        Debug.Log("Player die");

        StartCoroutine(ExecuteAfterTime(1f));
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Debug.Log("i'm_dead");
        SceneManager.LoadScene("SampleScene");
    }
   
}

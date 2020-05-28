using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody arrowBody;
    private float lifeTimer = 2f;
    private float timer;
    private bool hitSomething = false;
   
    void Start()
    {
        arrowBody = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.LookRotation(arrowBody.velocity);
    }

    
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifeTimer)
        {
            Destroy(gameObject);
        }

        if(!hitSomething)
            transform.rotation = Quaternion.LookRotation(arrowBody.velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Arrow")
        {
            hitSomething = true;
            Stick();
        }
      
    }

    private void Stick()
    {
        arrowBody.constraints = RigidbodyConstraints.FreezeAll;
    }
}

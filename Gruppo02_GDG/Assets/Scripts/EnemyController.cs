using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float playerDistance;
    public float awareAI = 10f;
    public float AIMoveSpeed;
    public float damping = 6.0f;

    public Transform[] navPoint;
    public NavMeshAgent agent;
    public int destPoint = 0;
    public Transform goal;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;

        agent.autoBraking = false;
        MoveToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(player.position, transform.position);

        if (playerDistance < awareAI)
        {
            LookAtPlayer();
            Debug.Log("SeenPlayer");
        }

        if (playerDistance < awareAI)
        {
            if (playerDistance > 2f)
            {
                Chase();
            }
            else { 
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    MoveToNextPoint();
                }
            }
        }

        void LookAtPlayer()
        {
            transform.LookAt(player);
        }

        void Chase()
        {
            transform.Translate(Vector3.forward * AIMoveSpeed * Time.deltaTime);
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        if (navPoint.Length == 0)
            return;
        agent.destination = navPoint[destPoint].position;
        destPoint = (destPoint + 1) % navPoint.Length;

        Debug.Log("Going to" + destPoint);
    }
}

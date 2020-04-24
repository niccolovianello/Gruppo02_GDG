using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float playerDistance;
    public float awareAI = 12f;
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

        Vector3 targetDir = player.position - transform.position;
        float angleToPlayer = (Vector3.Angle(targetDir, transform.forward));

        if (playerDistance < awareAI && angleToPlayer >= -60 && angleToPlayer <= 60)
        {
            LookAtPlayer();
        }

        if (playerDistance < awareAI && angleToPlayer >= -60 && angleToPlayer <= 60)
        {
            if (playerDistance > 2f)
            {
                //Chase();
                StartCoroutine(ExecuteAfterTime(2f));
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

       

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        if (navPoint.Length == 0)
            return;
        agent.destination = navPoint[destPoint].position;
        destPoint = (destPoint + 1) % navPoint.Length;
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Chase();
    }

    void Chase()
    {
        transform.Translate(Vector3.forward * AIMoveSpeed * Time.deltaTime);
    }
}

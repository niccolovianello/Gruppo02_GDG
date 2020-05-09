using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine.Animations;

public class EnemyController : MonoBehaviour
{
    public Transform[] navPoint;
    public NavMeshAgent agent;
    public Transform goal;
    public Transform player;
    private Animator anim;
   
    public float playerDistance;
    public float awareAI = 14f;
    public float AIMoveSpeed;
    public float damping = 6.0f;    
    public int destPoint = 0;
    
    //bool ischasing = false;
    float angleToPlayer;


    //variabili animazione
    
    
    


    
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = goal.position;
        agent.autoBraking = false;
        MoveToNextPoint();
        
        // controllo su animator
        anim = this.GetComponent<Animator>();
        if (anim == null)
        {
            Debug.Log("no animator");
            return;
        }
    }

  
    void Update()
    {
        playerDistance = Vector3.Distance(player.position, transform.position);
        Vector3 targetDir = player.position - transform.position;
        angleToPlayer = (Vector3.Angle(targetDir, transform.forward));

        if (playerDistance < awareAI && angleToPlayer >= -60 && angleToPlayer <= 60)
        {
            LookAtPlayer();
        }

        if (playerDistance < awareAI && angleToPlayer >= -60 && angleToPlayer <= 60)
        {
            if (playerDistance > 2f)
            {
                //Chase();
                //ischasing = true;
                StartCoroutine(ExecuteAfterTime(2f));
            }
            else { 
            //if (!agent.pathPending && agent.remainingDistance < 0.5f)
                //{
                    MoveToNextPoint();
                //}
            }
        }

        void LookAtPlayer()
        {
            /*if(ischasing == false)
            {
                transform.DOLookAt(player.position, 1.0f);
            }
            else
            {
                transform.LookAt(player);
            }*/
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            MoveToNextPoint();
        }
    }

    void MoveToNextPoint()
    {
        if (agent.stoppingDistance != 0)
            agent.stoppingDistance = 0;
        if (agent.speed == AIMoveSpeed)
            agent.speed = 3.4f;

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
        //transform.Translate(Vector3.forward * AIMoveSpeed * Time.deltaTime);
        agent.SetDestination(goal.position);
        agent.speed = AIMoveSpeed;
        agent.stoppingDistance = 3f;
    }

    private void UpdateAnimation() // metodo dove implementare animazioni  da richiamare dell'update
    { 
     
        if(!isFollowingPlayer)
    }

}

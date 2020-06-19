using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine.Animations;
using System.Runtime.Serialization;
//using System.Diagnostics;

namespace Com.Kawaiisun.SimpleHostile 
{

    public class EnemyController : MonoBehaviour
    {
        public Transform[] navPoint;
        public Vector3[] navPointPos;
        public NavMeshAgent agent;
        Transform goal;
        Transform player;
        public Animator anim;

        float playerDistance;
        public float awareAI = 14f;
        public float angleAwareness;
        public float AIMoveSpeed;
        //public float damping = 6.0f;    
        public int destPoint = 0;

        bool ischasing = false;
        bool isfollowing = false;
        float angleToPlayer;
        float startSpeed;

        float timer = 15f; //tempo inseguimento se non visto
        float timeleft;

        //bool seenplayer = false; // FIX THIS
        bool closeattack = false;

        public GameObject monster_Right_Fist;

        public ObjectsManagement obj;

        private AudioManager aud;

        bool seen = false;
        bool isAttacking = false;

        bool dead = false;

        //public Transform attackPoint;
        //public float attackRange;
        //public LayerMask playerLayer;
        //public int timeToAttack;
        //Coroutine currentCoroutine;
        //public bool attackFlag;

        //variabili animazione

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.autoBraking = false;
            startSpeed = agent.speed;
            timeleft = timer;
            MoveToNextPoint();
            obj = FindObjectOfType<ObjectsManagement>();
            angleAwareness = 40f;

            player = GameObject.Find("Player").transform;
            goal = GameObject.Find("Player").transform;
            if (player == null || goal == null)
                Debug.Log("not found player");

            //// controllo su animator
            //anim = this.GetComponent<Animator>();
            agent.acceleration = 1000f;
        }

        void Start()
        {
            aud = FindObjectOfType<AudioManager>();
            if (navPoint.Length > 0)
            {
                navPointPos = new Vector3[navPoint.Length];
                for (int i = 0; i < navPointPos.Length; i++)
                {
                    navPointPos[i] = navPoint[i].gameObject.GetComponent<PatrolPointsRaycast>().GetPPLocation();
                }
            }
            else
            {
                return;
            }

            agent = GetComponent<NavMeshAgent>();
            agent.autoBraking = false;
            startSpeed = agent.speed;
            timeleft = timer;
            MoveToNextPoint();
            obj = FindObjectOfType<ObjectsManagement>();
            angleAwareness = 40f;

            player = GameObject.Find("Player").transform;
            goal = GameObject.Find("Player").transform;
            if (player == null || goal == null)
                Debug.Log("not found player");

            //// controllo su animator
            //anim = this.GetComponent<Animator>();
            agent.acceleration = 1000f;

            // ++volume audioclip
            AudioClip inAudioClip = this.GetComponent<AudioSource>().clip;
            float[] clipSampleData = new float[inAudioClip.samples];
            inAudioClip.GetData(clipSampleData, 0);
            for (int s = 0; s < clipSampleData.Length; s++)
            {
                clipSampleData[s] = clipSampleData[s] * 1.3f;
            }
            inAudioClip.SetData(clipSampleData, 0);
        }

        void Update()
        {
            // controllo su animator
            if (anim == null)
            {
                Debug.Log("no animator");
                return;
            }
            if (anim != null)
                UpdateAnimations();

            //if (attackFlag == false)
            //{
            //    if(currentCoroutine != null)
            //        StopCoroutine(currentCoroutine);
            //}
            playerDistance = Vector3.Distance(player.position, transform.position);
            Vector3 targetDir = player.position - transform.position;
            angleToPlayer = (Vector3.Angle(targetDir, transform.forward));

            if (obj.getCurrentObj() != null)
            {
                if (!obj.getCurrentObj().name.StartsWith("Bow"))
                {
                    Light objLight = obj.getCurrentObj().GetComponentInChildren<Light>();

                    if (objLight.enabled == true)
                    {
                        angleAwareness = 60f;
                    }
                    else if (objLight.enabled == false)
                    {
                        angleAwareness = 40f;
                    }
                }
                else
                {
                    Transform arr = obj.getCurrentObj().transform.Find("ArrowSpawn");
                    Light objLight;

                    if (arr.childCount != 0)
                    {
                        objLight = arr.gameObject.GetComponentInChildren<Light>();

                        if (objLight == null)
                        {
                            return;
                        }
                        else
                        {
                            if (objLight.enabled == true)
                            {
                                angleAwareness = 60f;
                            }
                            else if (objLight.enabled == false)
                            {
                                angleAwareness = 40f;
                            }
                        }
                    }
                    else
                    {
                        angleAwareness = 40f;
                    }
                }
            }
            else
            {
                angleAwareness = 40f;
            }

            if (obj.getCurrentObj() != null)
            {
                if (obj.getCurrentObj().name.StartsWith("Lantern"))
                {
                    Light objLight = obj.getCurrentObj().GetComponentInChildren<Light>();

                    if (objLight.enabled == true)
                    {
                        agent.stoppingDistance = 3f;
                    }
                    else if (objLight.enabled == false)
                    {
                        agent.stoppingDistance = 0f;
                    }
                }
                else
                {
                    agent.stoppingDistance = 0f;
                }
            }
            else
            {
                agent.stoppingDistance = 0f;
            }

            if (playerDistance < 2f)
            {
                ////attackFlag = true;
                //closeattack = true;
                ////Debug.Log("enemyattack");
                //anim.SetTrigger("Attack"); // RISOLVERE PROBLEMA
                //                           //if(attackFlag == true)
                //                           //    currentCoroutine = StartCoroutine(TimeToAttackMethod(timeToAttack));

                if (!isAttacking)
                {
                    closeattack = true;
                    isAttacking = true;
                    anim.SetTrigger("Attack");
                    StartCoroutine(ResetBool(0.8f));
                }
            }
            else
            {
                closeattack = false;
            }

            // START BOOL DEAD
            if (!dead)
            {
                if ((playerDistance < awareAI && angleToPlayer >= -angleAwareness && angleToPlayer <= angleAwareness) || seen == true) //se in cono visivo (120) ed entro distanza
                {
                    //seenplayer = true;
                    timeleft = timer;
                    LookAtPlayer();

                    if (playerDistance > 2f)
                    {
                        //ischasing = true;
                        if (ischasing == false)
                        {
                            aud.Play("SeenByMonster");

                            ischasing = true;
                            isfollowing = false;

                            seen = false; //added

                            StartCoroutine(ExecuteAfterTime(2f));
                        }
                        else
                        {
                            Chase();
                        }
                    }
                    /*else
                    { 
                        MoveToNextPoint();
                    }*/
                }
                else if (ischasing == true) //timer inseguimento se non in cono visivo e in certa distanza
                {
                    if (timeleft > 0f)
                    {
                        timeleft -= Time.deltaTime;
                        //Debug.Log(Mathf.Round(timeleft));
                        Chase();
                    }
                    else //quando scade timer
                    {
                        ischasing = false;
                        isfollowing = false;
                    }
                }

                void LookAtPlayer()
                {
                    if (ischasing == false || isfollowing == false) //se non lo sta già inseguendo si ferma a guardare player
                    {
                        agent.speed = 0f;
                    }
                    Vector3 direction = (player.position - transform.position).normalized;
                    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

                    StartCoroutine(FollowAfterTime(1.5f)); //dopo 2 secondi inizia a seguire player
                }

                if (!agent.pathPending && agent.remainingDistance < 0.5f && ischasing == false) //patrol
                {
                    MoveToNextPoint();
                    //Debug.Log(gameObject.name + "called from if pathpending");

                    //Debug.Log("called " + ischasing);
                }
            }
            else
            {
                ischasing = false;
                isfollowing = false;
                agent.isStopped = true;
            }
            // END BOOL DEAD

            if (anim != null)
            {
                if (/*anim.GetCurrentAnimatorStateInfo(1).IsName("Stabbing") ||*/ anim.GetCurrentAnimatorStateInfo(0).IsName("Stabbing"))
                {
                    agent.speed = 0f;
                }
                else
                {
                }
            }
            else
            {
                return;
            }
        }

        //END UPDATE

        private void UpdateAnimations()
        {
            anim.SetFloat("Speed", agent.speed);
            //anim.SetBool("SeenPlayer", seenplayer);
        }

        void MoveToNextPoint()
        {
            if (ischasing == true)
                ischasing = false;
            if (isfollowing == true)
                isfollowing = false;
            //if (agent.stoppingDistance != 0f) //tutti reset condizione patrol, non inseguimento
            //    agent.stoppingDistance = 0f;
            if (agent.speed == AIMoveSpeed)
                agent.speed = 3.4f;
            if (timeleft != timer)
                timeleft = timer;

            //seenplayer = false;

            if (navPointPos.Length == 0)
                return;
            agent.destination = navPointPos[destPoint];//.position;
            destPoint = (destPoint + 1) % navPointPos.Length;
        }

        IEnumerator ExecuteAfterTime(float time)
        {
            yield return new WaitForSeconds(time);

            Chase();
        }

        IEnumerator FollowAfterTime(float time)
        {
            yield return new WaitForSeconds(time);

            if (agent.speed < startSpeed && /*!anim.GetCurrentAnimatorStateInfo(1).IsName("Stabbing")*/ !anim.GetCurrentAnimatorStateInfo(0).IsName("Stabbing")) //aumento velocità da quando si ferma
            {
                agent.speed = startSpeed;
            }
            isfollowing = true;
            //Debug.Log("sta seguendo");
        }

        IEnumerator ResetBool(float time)
        {
            yield return new WaitForSeconds(time);

            isAttacking = false;
        }

        void Chase() //inseguimento player, ischasing settato prima, in update
        {
            //transform.Translate(Vector3.forward * AIMoveSpeed * Time.deltaTime);
            agent.SetDestination(goal.position);
            agent.speed = AIMoveSpeed;
            //agent.stoppingDistance = 0.7f; // REMOVED FOR NOW
        }

        //private void UpdateAnimation() // metodo dove implementare animazioni  da richiamare dell'update
        //{
        //    //if(!isFollowingPlayer)
        //}

        //private IEnumerator TimeToAttackMethod(int t_before_attack)
        //{
        //    yield return new WaitForSeconds(t_before_attack);
        //    Collider[] playerCollider = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
        //    if (playerCollider.Length != 0)
        //    {
        //        Debug.Log("colpisco player");
        //        Debug.Log(playerCollider[0]);
        //        playerCollider[0].GetComponent<PlayerHealthManager>().TakeDamage(30);
        //        closeattack = false;

        //    }
        //    attackFlag = false;
        //}

        public void activateFist()
        {
            monster_Right_Fist.GetComponent<Collider>().enabled = true;
        }

        public void deactivateFist()
        {
            monster_Right_Fist.GetComponent<Collider>().enabled = false;
        }

        public void SetNavSize(int s, Transform navP)
        {
            navPoint = new Transform[0];
            navPointPos = new Vector3[s];
            List<int> list = new List<int>();

            for (int n = 0; n < 5; n++)
            {
                list.Add(n);
            }

            for (int i = 0; i < s; i++)
            {
                int index = Random.Range(0, list.Count - 1);
                int nr = list[index];
                navPointPos[i] = navP.GetChild(nr).GetComponent<PatrolPointsRaycast>().GetPPLocation();
                list.RemoveAt(index);

                //Debug.Log(navP.GetChild(nr).name);
            }
        }

        public bool GetFollowing()
        {
            return isfollowing;
        }

        public bool GetChasing()
        {
            return ischasing;
        }

        public void Dead()
        {
            dead = true;
        }

        public void SetSeen()
        {
            seen = true;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(agent.destination, 0.5f);
        }

        public void SetHurt()
        {
            anim.SetTrigger("Hurt");
        }

        public void SetDie()
        {
            agent.speed = 0f;
            agent.isStopped = true;
            isfollowing = false;
            ischasing = false;
            anim.SetTrigger("Dead");
        }

        public void PlayAttack()
        {
            aud.Play("Attack");
        }
    } 
}

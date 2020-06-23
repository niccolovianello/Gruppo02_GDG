using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Com.Kawaiisun.SimpleHostile
{

    public class PlayerLife : MonoBehaviour
    {
        float timeLeft = 30f;
        float plCurrentTime;

        public ObjectsManagement obj;

        public UIScript UI;

        bool followed = false;

        private AudioManager aud;
        bool isplaying = true;

        // Start is called before the first frame update
        void Start()
        {
            plCurrentTime = timeLeft;
            obj = FindObjectOfType<ObjectsManagement>();
            aud = FindObjectOfType<AudioManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (obj.getCurrentObj() != null)
            {
                if (!obj.getCurrentObj().name.StartsWith("Bow"))
                {

                    Light objLight = obj.getCurrentObj().GetComponentInChildren<Light>();

                    if (objLight.enabled == true)
                    {
                        if (plCurrentTime < 30f && plCurrentTime > 0f)
                        {
                            plCurrentTime += Time.deltaTime;

                            UI.TimerDarkUI(plCurrentTime);
                        }
                    }
                    else if (objLight.enabled == false)
                    {
                        //if(plCurrentTime > 0f)
                        //{
                        plCurrentTime -= Time.deltaTime;

                        UI.TimerDarkUI(plCurrentTime);
                        //}
                    }
                }
                else
                {
                    Transform arr = obj.getCurrentObj().transform.Find("ArrowSpawn");

                    Light objLight;

                    objLight = arr.gameObject.GetComponentInChildren<Light>();

                    if (objLight == null)
                    {
                        plCurrentTime -= Time.deltaTime;

                        UI.TimerDarkUI(plCurrentTime);
                    }
                    else
                    {
                        if (objLight.enabled == true)
                        {
                            if (plCurrentTime < 30f && plCurrentTime > 0f)
                            {
                                plCurrentTime += Time.deltaTime;

                                UI.TimerDarkUI(plCurrentTime);
                            }
                        }
                        else if (objLight.enabled == false)
                        {
                            plCurrentTime -= Time.deltaTime;

                            UI.TimerDarkUI(plCurrentTime);
                        }
                    }
                }
            }
            else
            {
                plCurrentTime -= Time.deltaTime;

                UI.TimerDarkUI(plCurrentTime);
            }

            //Debug.Log(Mathf.Round(plCurrentTime));
            if(plCurrentTime < 0f)
            {
                Die();

            }

            if (SetFollowed() && !isplaying)
            {
                aud.Play("Chasing");
                isplaying = true;
            }
            else if(!SetFollowed() && isplaying)
            {
                aud.Stop("Chasing");
                isplaying = false;
            }
        }

        void Die()
        {
            //Debug.Log("death_darkness");
            UI.TimerDarkUI(255);

            StartCoroutine(ExecuteAfterTime(1f)); //delete and destroy gameobject?
        }

        IEnumerator ExecuteAfterTime(float time)
        {
            yield return new WaitForSeconds(time);

            Debug.Log("i'm_dead");
            Destroy(gameObject);
            SceneManager.LoadScene("SampleScene");
        }

        public bool SetFollowed()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");

            //Debug.Log(enemies.Length);

            foreach(GameObject e in enemies)
            {
                if (e.GetComponent<EnemyController>().GetChasing() || e.GetComponent<EnemyController>().GetFollowing())
                {
                    //Debug.Log(e.name + "isfoll or chas" + e.GetComponent<EnemyController>().GetChasing() + "CHAS" + e.GetComponent<EnemyController>().GetFollowing() + "FOLL");
                    
                    return true;
                }
            }
            return false;
        }

        //public bool GetFollowed()
        //{
        //    return followed;
        //}
    }
}

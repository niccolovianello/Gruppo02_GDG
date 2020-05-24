using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Kawaiisun.SimpleHostile
{
    public class PatrolPointsData : MonoBehaviour
    {
        GameObject patrol;
        GameObject[] patrPoints;

        // Start is called before the first frame update
        void Start()
        {
            patrol = GameObject.Find("PatrolPoints");
            if (patrol == null)
                Debug.Log("not found PatrolPoints");
            patrPoints = new GameObject[patrol.transform.childCount];
            for (int i = 0; i < patrol.transform.childCount; i++)
            {
                patrPoints[i] = patrol.transform.GetChild(i).gameObject;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public GameObject ChoosePNum() // returns cluster farther from player TO CHANGE WITH PROBABILITY THAT FILLS RANDOM RANGE
        {
            GameObject MaxP = null;
            float maxDist = 0;
            Vector3 currentPos = transform.position;
            foreach (GameObject g in patrPoints)
            {
                float dist = Vector3.Distance(g.transform.position, currentPos);
                if (dist > maxDist)
                {
                    MaxP = g;
                    maxDist = dist;
                }
            }
            return MaxP;


            //int GetRandomValue()
            //{
            //    float rand = Random.value;
            //    if (rand <= .5f)
            //        return Random.Range(0, 6);
            //    if (rand <= .8f)
            //        return Random.Range(6, 9);

            //    return Random.Range(9, 11);
            //}

            //var r = Random.Range(0, 100);
            //if (r < 20)
            //{
            //    scene = Random.Range(1, 10);
            //}
            //else if (r >= 20 && r < 40)
            //{
            //    scene = Random.Range(11, 20);
            //}
            //else if (r >= 40)
            //{
            //    scene = Random.Range(21, 30);
            //}

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor.Animations;

namespace Com.Kawaiisun.SimpleHostile
{
    public class PatrolPointsData : MonoBehaviour
    {
        public GameObject player;
        public GameObject enemy;
        EnemyController ec;
        GameObject[] patrPoints;
        int deadEnemies;
        GameObject PSpawn;

        // Start is called before the first frame update
        void Start()
        {
            patrPoints = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                patrPoints[i] = transform.GetChild(i).gameObject;
            }

            deadEnemies = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L)) // PUT THIS WHERE IT BELONGS AND MAKE CONTROL SETDEADENEMIES 1-4
            {
                SortedArray();
                ChooseSpawn();
                SpawnEnemy();
            }
        }

        public void SortedArray() // sorted array, far > near
        {
            patrPoints = patrPoints.OrderBy(patrPoints => (patrPoints.transform.position - player.transform.position).sqrMagnitude).ToArray();
        }

        public void SetDeadEnemies() // set when one enemy dies
        {
            if(deadEnemies < 4)
            {
                deadEnemies += deadEnemies;
            }
            else
            {
                deadEnemies = 0;
            }
        }

        public GameObject ChooseSpawn() //choose cluster P# where to spawn
        {
            foreach (GameObject p in patrPoints)
            {
                GameObject enemiesEmpty = p.transform.Find("Enemies").gameObject;
                if (enemiesEmpty.transform.childCount < 4)
                {
                    PSpawn = p;
                    break;
                }
            }
            return PSpawn;
        }

        public void SpawnEnemy()
        {
            ec = enemy.GetComponent<EnemyController>();
            ec.SetNavSize(Random.Range(2,5), PSpawn.transform);
            var newenemy = Instantiate(enemy, PSpawn.transform.position, Quaternion.identity);
            newenemy.transform.parent = PSpawn.transform.Find("Enemies");
        }
    }
}

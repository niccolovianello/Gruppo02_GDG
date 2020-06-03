using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
namespace Com.Kawaiisun.SimpleHostile
{
    [CreateAssetMenu(fileName = "New Object", menuName = "Equipment")]
    public class Equipment : ScriptableObject
    {

        public string name;
        public GameObject prefab;
        public Vector3 eq_position;
        public Vector3 eq_rotation;
        public float brightness;
        public float attackRange;
        public int damage;
        public float attackRate = 1f;
        public bool isSelected = false;
        public float charge = 0;
       

    }
}
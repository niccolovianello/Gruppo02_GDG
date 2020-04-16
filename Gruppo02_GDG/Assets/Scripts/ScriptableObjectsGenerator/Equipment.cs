using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.Kawaiisun.SimpleHostile
{
    [CreateAssetMenu(fileName = "New Object", menuName = "Equipment")]
    public class Equipment : ScriptableObject
    {

        public string name;
        public GameObject prefab;
        public float aimSpeed;
        public float brightness;

    }
}
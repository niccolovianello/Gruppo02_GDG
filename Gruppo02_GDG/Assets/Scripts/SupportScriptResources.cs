using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Kawaiisun.SimpleHostile
{
    public class SupportScriptResources : MonoBehaviour
    {
        private float remainEnergy = 50;
        private float remainOil = 50;
        private float remainLifeTorch = 50;

        public float GetRemainEnergy()
        {
            return remainEnergy;
        }
        public float GetRemainOil()
        {
            return remainOil;
        }
        public float GetRemainLifeTorch()
        {
            return remainLifeTorch;
        }
        public void SetRemainLifeTorch(float r)
        {
            remainLifeTorch = r;
        }
        public void SetRemainOil(float o)
        {
            remainOil = o;
        }
        public void SetRemainEnergy(float e)
        {
            remainEnergy = e;
        }

    }
}

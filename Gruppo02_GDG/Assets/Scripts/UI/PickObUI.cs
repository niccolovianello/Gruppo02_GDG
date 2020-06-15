using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Kawaiisun.SimpleHostile
{
    public class PickObUI : MonoBehaviour
    {
        Image dot;

        void Start()
        {
            dot = this.GetComponent<Image>();
        }

        public void DotEnlight()
        {
            dot.color = new Color32(103, 255, 0, 255);
                //new Color32(255, 249, 87, 255);
        }

        public void DotNormal()
        {
            dot.color = new Color32(190, 190, 190, 145);
        }
    }
}

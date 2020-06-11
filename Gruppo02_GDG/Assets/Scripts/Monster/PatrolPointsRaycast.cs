using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com.Kawaiisun.SimpleHostile
{
    public class PatrolPointsRaycast : MonoBehaviour // Raycast PatrolPoints-Ground
    {
        Vector3 ppLoc;

        void Awake()
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Ground")))
            {
                //Debug.Log(gameObject.name + " " + hit.point);
                ppLoc = hit.point;
            }
            else
            {
                Debug.Log("no ground under");
                gameObject.SetActive(false);
            }

            ppLoc = new Vector3(ppLoc.x, ppLoc.y + 1.5f, ppLoc.z);
            //Debug.Log(gameObject.name + "new: " + ppLoc);
        }

        public Vector3 GetPPLocation()
        {
            //ppLoc.y += 1;
            return ppLoc;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(ppLoc, 0.5f);
        }
    }
}

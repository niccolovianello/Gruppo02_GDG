using Com.Kawaiisun.SimpleHostile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class FallRespawn : MonoBehaviour
{
    Transform DeathPanel;

    private void Start()
    {
        DeathPanel= GameObject.Find("CanvasUI").transform.Find("DeathPanel");
        if (DeathPanel == null)
            Debug.Log("not found DeathPanel from TriggerFall");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player") == true)
        {
            DeathPanel.GetComponent<Image>().DOColor(new Color32(0, 0, 0, 255), 2f);

            StartCoroutine(ExecuteAfterTime(2.2f));
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Debug.Log("i'm_dead");
        SceneManager.LoadScene("SampleScene");
    }
}

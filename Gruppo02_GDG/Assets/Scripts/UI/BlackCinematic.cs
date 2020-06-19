using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BlackCinematic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAfter(6f));
    }

    IEnumerator StartAfter(float wait)
    {
        yield return new WaitForSeconds(wait);

        this.GetComponent<Image>().DOColor(new Color32(255, 255, 255, 255), 1.5f);
    }
}

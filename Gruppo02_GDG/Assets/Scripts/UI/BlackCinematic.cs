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
        StartCoroutine(StartAfter(6.5f));
    }

    IEnumerator StartAfter(float wait)
    {
        yield return new WaitForSeconds(wait);

        this.GetComponent<Image>().DOColor(new Color32(0, 0, 0, 255), 1f);
    }
}

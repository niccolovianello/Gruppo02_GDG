using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginScript : MonoBehaviour
{
    
    
    
    void Start()
    {
        StartCoroutine(FiniscCut());
    }

    IEnumerator FiniscCut()
    { 
        yield return new WaitForSeconds(7.5f);
        Debug.Log("gira");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

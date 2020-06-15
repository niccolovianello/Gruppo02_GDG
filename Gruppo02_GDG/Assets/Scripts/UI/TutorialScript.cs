using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] private GameObject TutorialCanvas;
    bool isActive = true;

    public GameObject pauseMenuUI;
    
     void Awake()
    {
        if (isActive == true)
        {
            StartCoroutine(StartAfter(0.2f));
        }
        else
        {
            SetCanvActive(false);
        }
    }

    public void SetCanvActive(bool act)
    {
        TutorialCanvas.SetActive(act);
        isActive = act;

        if(act == false && !pauseMenuUI.gameObject.activeSelf)
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    IEnumerator StartAfter(float time)
    {
        yield return new WaitForSeconds(time);

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

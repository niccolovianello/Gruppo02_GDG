using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Kawaiisun.SimpleHostile
{
    public class TutorialScript : MonoBehaviour
    {
        public static TutorialScript inst = null;
        [SerializeField] private GameObject TutorialPanel;
        [SerializeField] private GameObject TutorialPanelL;
        [SerializeField] private GameObject TutorialPanelF;
        [SerializeField] private GameObject TutorialPanelT;
        [SerializeField] private GameObject TutorialPanelB;
        bool isActive = true;
        bool isActiveL = true;
        bool isActiveF = true;
        bool isActiveT = true;
        bool isActiveB = true;

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

        void Start()
        {
            if (inst == null)
            {
                inst = this;
            }
            else if (inst != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        public void SetCanvActive(bool act)
        {
            TutorialPanel.SetActive(act);
            isActive = act;

            if (act == false)
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        public void SetCanvNotActiveFromPause(bool disable)
        {
            TutorialPanel.SetActive(disable);
            isActive = disable;
        }

        public void SetWeaponActive(string str)
        {
            if (str.StartsWith("F") && isActiveF == true)
            {
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                TutorialPanelF.SetActive(true);
                //TutorialPanelF.transform.Find("FillImage").GetComponent<Image>().DOFillAmount(0,4);
                isActiveF = false;
                //StartCoroutine(DeactivateAfter(TutorialPanelF, 4f));
            }
            else if (str.StartsWith("T") && isActiveT == true)
            {
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                TutorialPanelT.SetActive(true);
                //TutorialPanelT.transform.Find("FillImage").GetComponent<Image>().DOFillAmount(0, 4);
                isActiveT = false;
                //StartCoroutine(DeactivateAfter(TutorialPanelT, 4f));
            }
            else if (str.StartsWith("L") && isActiveL == true)
            {
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                TutorialPanelL.SetActive(true);
                //TutorialPanelL.transform.Find("FillImage").GetComponent<Image>().DOFillAmount(0, 4);
                isActiveL = false;
                //StartCoroutine(DeactivateAfter(TutorialPanelL, 4f));
            }
            else if (str.StartsWith("B") && isActiveB == true)
            {
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                TutorialPanelB.SetActive(true);
                //TutorialPanelB.transform.Find("FillImage").GetComponent<Image>().DOFillAmount(0, 4);
                isActiveB = false;
                //StartCoroutine(DeactivateAfter(TutorialPanelB, 4f));
            }
        }

        IEnumerator StartAfter(float time)
        {
            yield return new WaitForSeconds(time);

            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //IEnumerator DeactivateAfter(GameObject namepanel, float time)
        //{
        //    yield return new WaitForSeconds(time);

        //    namepanel.SetActive(false);
        //    //Time.timeScale = 1;
        //}

        public void DeactivatePanel(GameObject panel)
        {
            panel.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void SetBoolActive(bool active)
        {
            isActive = active;
        }
    }
}

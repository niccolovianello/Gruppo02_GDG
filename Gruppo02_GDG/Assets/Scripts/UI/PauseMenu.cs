using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.Kawaiisun.SimpleHostile
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenuUI;

        [SerializeField] private bool isPaused;

        public GameObject TutCanvas;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = !isPaused;

                ActivateMenu(isPaused);
            }
        }

        public void QuitBtn()
        {
            Debug.Log("Quit");
            Application.Quit();
        }

        public void BackToMainBtn()
        {
            SceneManager.LoadScene(0);
        }

        public void ActivateMenu(bool activate)
        {
            if (activate)
            {
                Time.timeScale = 0;
                pauseMenuUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //AudioListener.pause = true;

                if (TutCanvas.gameObject.activeSelf)
                    TutCanvas.GetComponent<TutorialScript>().SetCanvActive(false);
                return;
            }
            else
            {
                Time.timeScale = 1;
                pauseMenuUI.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                //AudioListener.pause = false;
                isPaused = false;
                return;
            }
        }
    }
}

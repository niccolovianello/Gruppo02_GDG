using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Com.Kawaiisun.SimpleHostile
{
    public class LevelControl : MonoBehaviour
    {
        public static LevelControl instance = null;
        bool unlocked;

        public Image picture01;
        public Image message01;
        public Image level01;

        TutorialScript tutScript;

        // Start is called before the first frame update
        void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            if (SceneManager.GetActiveScene().buildIndex == 1 && picture01 == null)
            {
                picture01 = GameObject.Find("Canvas/Background/LevelsMenu/FinalPicture/Part01").GetComponent<Image>();
                message01 = GameObject.Find("Canvas/Background/LevelsMenu/MessagesPanel/Message01").GetComponent<Image>();
                level01 = GameObject.Find("Canvas/Background/LevelsMenu/LevelList/L01").GetComponent<Image>();
            }
            if (picture01 == null && message01 == null && level01 == null)
                return;

            if (unlocked == true)
            {
                picture01.color = new Color32(255, 255, 255, 255);
                message01.enabled = true;
                level01.color = new Color32(202, 255, 166, 255);
                unlocked = false;
            }
        }

        public void Win()
        {
            SceneManager.LoadScene(1);
            unlocked = true;

            tutScript.SetBoolActive(true);
        }
    }
}

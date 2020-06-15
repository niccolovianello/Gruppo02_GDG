using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Kawaiisun.SimpleHostile
{
    public class EndPoint : MonoBehaviour
    {
        public GameObject EndCanvas;
        public LevelControl levelControl;

        // Start is called before the first frame update
        void Start()
        {
            levelControl = GameObject.Find("LevelControl").GetComponent<LevelControl>();
            if (levelControl == null)
                Debug.Log("not found LevControl from EndPoint");
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                EndCanvas.SetActive(true);

                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "Player")
            {
                EndCanvas.SetActive(true);

                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void SetWin()
        {
            levelControl.Win();
            Time.timeScale = 1;
        }
    }
}

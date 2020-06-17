using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioManager aud;

    private void Awake()
    {
        aud = FindObjectOfType<AudioManager>();
    }
    private void Start()
    {
        aud.Play("MenuTheme");
    }
    public void PlayBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitBtn()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}

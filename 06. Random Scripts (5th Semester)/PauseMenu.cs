using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //Pause functions owned by me. Source: https://answers.unity.com/questions/171492/how-to-make-a-pause-menu-in-c.html
    //Other functions related to opening canvas are mine.

    public static PauseMenu Instance;
    public Canvas pauseMenu;
    public string currentLevel;

    private bool paused;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

        pauseMenu.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            paused = togglePause();
        }
    }

    bool togglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            pauseMenu.enabled = false;
            return (false);
        }

        else
        {
            Time.timeScale = 0f;
            pauseMenu.enabled = true;
            return (true);
        }
    }


    public void Restart()
    {
        pauseMenu.enabled = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentLevel);
    }

    public void Exit()
    {
        pauseMenu.enabled = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}

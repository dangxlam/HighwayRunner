using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{

    public GameObject PausePanel;

    public void ReplayGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void QuitGame ()
    {
        Application.Quit();
    }


    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;

    } 

    public void Resume()
    {
        PausePanel?.SetActive(false);
        Time.timeScale = 1f;
    }
    
}

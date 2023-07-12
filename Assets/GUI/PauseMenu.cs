using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
        SampleTeleporterController.Active = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Tutorial");
        PlayerGUI.CurrentHealth = (int)PlayerGUI.MaxHealth;
        PlayerGUI.CurrentEnergy = (int)PlayerGUI.MaxEnergy;
        SampleTeleporterController.Active = true;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
        SampleTeleporterController.Active = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}

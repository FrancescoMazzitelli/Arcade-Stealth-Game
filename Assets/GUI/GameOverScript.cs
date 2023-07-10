using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Tutorial");
        PlayerGUI.CurrentHealth = (int)PlayerManager.MaxHealth;
        PlayerGUI.CurrentEnergy = (int)PlayerManager.MaxEnergy;
        SampleTeleporterController.Active = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Setdown()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

}

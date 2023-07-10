using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("Tutorial");
        PlayerGUI.CurrentHealth = (int)PlayerManager.MaxHealth;
        PlayerGUI.CurrentEnergy = (int)PlayerManager.MaxEnergy;
        SampleTeleporterController.Active = true;
    }

    public void Shop()
    {

    }
}

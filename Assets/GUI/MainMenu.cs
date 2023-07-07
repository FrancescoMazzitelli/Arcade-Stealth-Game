using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("Tutorial");
        PlayerGUI.Health = 100;
        PlayerGUI.Energy = 100;
        SampleTeleporterController.Active = true;
    }

    public void Shop()
    {

    }
}

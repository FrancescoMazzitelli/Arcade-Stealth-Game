using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishScript : MonoBehaviour
{
    public GameObject levelComplete;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelComplete.SetActive(true);
            Time.timeScale = 0f;
            SampleTeleporterController.Active = false;
        }
    }

    public void NextLevel()
    {
        // Provvisorio
        // Qui bisogna collegare lo script di generazione procedurale del livello
        SceneManager.LoadScene("Tutorial");
        PlayerGUI.CurrentHealth = (int)PlayerManager.MaxHealth;
        PlayerGUI.CurrentEnergy = (int)PlayerManager.MaxEnergy;
        SampleTeleporterController.Active = true;
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
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

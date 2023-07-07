using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameOverScript GameOverScript;
    private Camera mainCamera;

    public void GameOver()
    {
        if(PlayerGUI.Health <= 0)
        {
            GameOverScript.Setup();
        }
            
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
    }

    private void Awake()
    {
        GameOverScript.Setdown();
    }


}

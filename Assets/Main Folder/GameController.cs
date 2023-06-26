using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameOverScript GameOverScript;
    private Camera mainCamera;

    public void GameOver()
    {
        if(LaserManager.Health <= 0)
        {
            GameOverScript.Setup();
            vThirdPersonCamera script = mainCamera.GetComponent<vThirdPersonCamera>();
            script.lockCamera = true;
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

    }


}

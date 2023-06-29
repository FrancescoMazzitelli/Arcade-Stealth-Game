using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleTeleporterController : MonoBehaviour
{
    public static SampleTeleporterController instance;
    public static bool active;

    public Teleporter teleporter;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        teleporter.Start();
    }

    void Update()
    {
        if(PlayerGUI.Energy > 0)
        {
            Active = true;
            if (Input.GetMouseButtonDown(0))
            {
                teleporter.DisplayArch(true);
            }

            if (Input.GetMouseButtonUp(0))
            {
                StartCoroutine("Controller");
            }
        }
        if (PlayerGUI.Energy <= 0)
        {
            Active = false;
        }

        if (PlayerGUI.Health <= 0)
        {
            Active = false;
        }
    }

    IEnumerator Controller()
    {
        if(GameObject.Find("PlayerController").GetComponent<vThirdPersonInput>().enabled == true)
        {
            GameObject.Find("PlayerController").GetComponent<vThirdPersonInput>().enabled = false;
            yield return new WaitForSeconds(0.02f);
            teleporter.Teleport();
            teleporter.DisplayArch(false);
            yield return new WaitForSeconds(0.02f);
            GameObject.Find("PlayerController").GetComponent<vThirdPersonInput>().enabled = true;
            PlayerGUI.Energy -= 20;
        }        
    }

    public void DisableScript()
    {
        enabled = false;
    }

    public static bool Active
    {
        get { return active; }
        set { active = value; }
    }
}


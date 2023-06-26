using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleTeleporterController : MonoBehaviour
{
    public static SampleTeleporterController instance;
    private static int currentEnergy = 100;
    public static bool active = true;

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
        if(currentEnergy > 0)
        {
            active = true;
            if (Input.GetMouseButtonDown(0))
            {
                teleporter.DisplayArch(true);
            }

            if (Input.GetMouseButtonUp(0))
            {
                StartCoroutine("Controller");
            }
        }
        else
        {
            active = false;
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
            currentEnergy = currentEnergy - 20;
        }        
    }

    public void DisableScript()
    {
        enabled = false;
    }

    public static int Energy
    {
        get { return currentEnergy; }
        set { currentEnergy = value; }
    }

    public static bool Active
    {
        get { return active; }
        set { active = value; }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleTeleporterController : MonoBehaviour
{

    public Teleporter teleporter;

    void Start()
    {
        teleporter.Start();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            teleporter.DisplayArch(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine("Controller");
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
        }        
    }
}


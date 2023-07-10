﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleTeleporterController : MonoBehaviour
{
    public static SampleTeleporterController instance;
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
        if(PlayerGUI.CurrentEnergy > 0)
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
            PlayerGUI.CurrentEnergy -= 20;
        }        
    }

    public void DisableScript()
    {
        enabled = false;
    }

    public void EnableScript()
    {
        enabled = true;
    }

    public static bool Active
    {
        get { return active; }
        set { active = value; }
    }
}


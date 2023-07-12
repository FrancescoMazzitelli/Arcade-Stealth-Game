using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerComponentManager manager;
    public static GameObject player;
    private static bool key = false;
    private static Transform playerTransform;

    void Awake()
    {
        manager = PlayerComponentManager.Instance;
        player = GameObject.FindGameObjectWithTag("Player");

        foreach (PlayerModifier modifier in manager.Modifiers)
        {
            if (modifier.Enabled == true)
            {
                PlayerFeature feature = modifier.Feature;

                feature.BaseValue = feature.BaseValue + modifier.AddFactor;
                feature.CurrentValue = feature.BaseValue;
                modifier.Enabled = false;
                manager.Features[feature.Name] = feature;
            }
        }
    }

    void Start()
    {
        Debug.Log("Max Health" + PlayerGUI.MaxHealth);
        Debug.Log("Max Energy" + PlayerGUI.MaxEnergy);
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerGUI.CurrentEnergy <= 0 && SampleTeleporterController.Active == true)
        {
            SampleTeleporterController.Active = false;
        }

        if (PlayerGUI.CurrentHealth <= 0 && SampleTeleporterController.Active == true)
        {
            SampleTeleporterController.Active = false;
        }

        //--------------------------------------------------------------------------------------------//


        if (SampleTeleporterController.Active == false)
        {
            SampleTeleporterController.instance.DisableScript();
        }

        if (SampleTeleporterController.Active == true)
        {
            SampleTeleporterController.instance.EnableScript();
        }

        if (ObjectInteraction.Interaction == true)
        {
            key = true;
        }
    }

    public static GameObject Player
    {
        get { return player; }
        set { player = value; }
    }

    public static Transform PlayerTransform
    {
        get { return playerTransform; }
        set { playerTransform = value; }
    }

    public static bool Key
    {
        get { return key; }
        set { key = value; }
    }
}

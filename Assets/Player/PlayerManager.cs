using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    private static float maxHealth;
    private static float maxEnergy;

    public PlayerComponentManager manager;
    public static GameObject player;
    private static bool key = false;
    private static Transform playerTransform;

    void Awake()
    {
        manager = PlayerComponentManager.Instance;
        player = GameObject.FindGameObjectWithTag("Player");

        foreach (PlayerFeature feature in manager.Features)
        {
            if (feature.Name.Contains("Health"))
            {
                maxHealth = feature.BaseValue;
            }
            if (feature.Name.Contains("Energy"))
            {
                maxEnergy = feature.BaseValue;
            }
        }

        foreach (PlayerModifier modifier in manager.Modifiers)
        {
            if (modifier.Enabled == true)
            {
                PlayerFeature feature = modifier.Feature;

                feature.CurrentValue = feature.CurrentValue + modifier.AddFactor;
                float newValue = feature.CurrentValue;
                modifier.Enabled = false;
                if (feature.Name.Contains("Health"))
                {
                    if (maxHealth > 0)
                        maxHealth = feature.CurrentValue;
                }
                if (feature.Name.Contains("Energy"))
                {
                    if (maxEnergy > 0)
                        maxEnergy = feature.CurrentValue;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

    public static float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public static float MaxEnergy
    {
        get { return maxEnergy; }
        set { maxEnergy = value; }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static GameObject player;
    private static bool key = false;
    private static Transform playerTransform;
    private Dictionary<string, string> playerParams = new Dictionary<string, string>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();

        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].name.Equals("Main character"))
                Player = gameObjects[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SampleTeleporterController.Active == false)
        {
            SampleTeleporterController.instance.DisableScript();
        }

        if(ObjectInteraction.Interaction == true)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemiesManager : MonoBehaviour
{
    public static string currentState;
    public static string previousState;

    public EnemyComponentManager manager;

    private GameObject enemiesContainer;
    private static List<GameObject> enemies ;
    private Dictionary<string, string> enemiesParams = new Dictionary<string, string>();
    public static float range;
    public static float detectionRange;
    private static GameObject player;
    private static GameObject laser;

    void Awake()
    {
        manager = new EnemyComponentManager();
        manager.Bind();

        enemies = new List<GameObject>();

        enemiesContainer = GameObject.FindGameObjectWithTag("Enemies");

        GameObject playerContainer = GameObject.FindGameObjectWithTag("Player");
        Transform tmpPlayer = playerContainer.transform.GetChild(0);
        player = tmpPlayer.gameObject;

        laser = GameObject.FindGameObjectWithTag("Laser");

        int childCount = enemiesContainer.transform.childCount;


        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = enemiesContainer.transform.GetChild(i);
            GameObject childObject = childTransform.gameObject;
            Transform tmpDrone = childObject.transform.GetChild(0);
            GameObject drone = tmpDrone.gameObject;
            enemies.Add(drone);
        }

        SendScripts();

        foreach (EnemyModifier modifier in manager.Modifiers)
        {
            if (modifier.GetName.Equals("Patrolling"))
                modifier.Enabled = true;
        }
    }

    void SendScripts()
    {
        foreach (GameObject drone in enemies)
        {
            foreach (EnemyFeature feature in manager.Features)
            {
                System.Type scriptType = feature.FeatureScript.GetType();

                if (drone.GetComponent(scriptType) == null)
                {
                    drone.AddComponent(scriptType);
                    Behaviour script = drone.GetComponent(scriptType) as Behaviour;
                    if (script != null)
                    {
                        script.enabled = false;
                    }
                }
            }
            laser.AddComponent<LaserManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject drone in enemies)
        {
            foreach(EnemyModifier modifier in manager.Modifiers)
            {
                if (modifier.Enabled)
                {
                    foreach(KeyValuePair<EnemyFeature, bool> entry in modifier.ScriptSelection)
                    {
                        EnemyFeature key = entry.Key;
                        System.Type scriptType = key.FeatureScript.GetType();
                        bool value = entry.Value;

                        Behaviour script = drone.GetComponent(scriptType) as Behaviour;
                        if (script != null)
                        {
                            script.enabled = value;
                        }
                    }
                }
            }
        }
    }

    public static float Range
    {
        get { return range; }
        set { range = value; }
    }

    public static string CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    public static string PreviousState
    {
        get { return previousState; }
        set { previousState = value; }
    }

    public static float DetectionRange
    {
        get { return detectionRange; }
        set { detectionRange = value; }
    }
}

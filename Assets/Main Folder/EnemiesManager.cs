using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static string currentState;
    public static string previousState;

    private GameObject enemiesContainer;
    private static List<GameObject> enemies = new List<GameObject>();
    private Dictionary<string, string> enemiesParams = new Dictionary<string, string>(); 
    public static float range;
    public static float detectingRange;
    private static GameObject player;
    private static GameObject laser;

    void Start()
    {
        GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();

        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].name.Equals("Enemies")) 
                enemiesContainer = gameObjects[i];
            if (gameObjects[i].name.Equals("Main character"))
            {
                Transform tmpPlayer = gameObjects[i].transform.GetChild(0);
                player = tmpPlayer.gameObject;
            }
            if (gameObjects[i].name.Equals("Laser"))
            {
                laser = gameObjects[i];
            }
        }

        int childCount = enemiesContainer.transform.childCount;


        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = enemiesContainer.transform.GetChild(i);
            GameObject childObject = childTransform.gameObject;
            enemies.Add (childObject);
        }

        foreach(GameObject obj in enemies)
        {
            Transform tmpDrone = obj.transform.GetChild(0);
            GameObject drone  = tmpDrone.gameObject;

            Detecting.Player = player.transform;
            Detecting.DetectionRange = 10; //DEBUG. da sostituire con valore preso dal Main Manager
            drone.AddComponent<Detecting>();
            drone.GetComponent<Detecting>().enabled = true;

            Patrolling.Range = range;
            drone.AddComponent<Patrolling>();
            drone.GetComponent<Patrolling>().enabled = true;

            Shooting.Player = player.transform;
            drone.AddComponent<Shooting>();
            drone.GetComponent<Shooting>().enabled = false;

            laser.AddComponent<LaserManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject obj in enemies)
        {
            Transform tmpDrone = obj.transform.GetChild(0);
            GameObject drone = tmpDrone.gameObject;

            PreviousState = CurrentState;
            CurrentState = drone.GetComponent<Detecting>().State;
            Debug.Log(currentState);

            if (previousState == "Shooting" && currentState == "Patrolling")
            {
                drone.GetComponent<Patrolling>().enabled = true;
                drone.GetComponent<Shooting>().enabled = false;
            }
            else if (previousState == "Patrolling" && currentState == "Shooting")
            {
                drone.GetComponent<Patrolling>().enabled = false;
                drone.GetComponent<Shooting>().enabled = true;
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

    public static float DetectingRange
    {
        get { return detectingRange; }
        set { detectingRange = value; }
    }
}

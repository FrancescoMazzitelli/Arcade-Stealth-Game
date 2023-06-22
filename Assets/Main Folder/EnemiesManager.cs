using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static string currentState;

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
            Detecting.DetectionRange = 10;
            drone.AddComponent<Detecting>();
            drone.GetComponent<Detecting>().enabled = false;

            Patrolling.Range = range;
            drone.AddComponent<Patrolling>();
            drone.GetComponent<Patrolling>().enabled = false;

            Shooting.Player = player.transform;
            drone.AddComponent<Shooting>();
            drone.GetComponent<Shooting>().enabled = false;

            laser.AddComponent<LaserManager>();
            currentState = "Patrolling";
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case "Patrolling":
                Debug.Log("Patrolling");
                foreach (GameObject obj in enemies)
                {
                    Transform tmpDrone = obj.transform.GetChild(0);
                    GameObject drone = tmpDrone.gameObject;
                    drone.GetComponent<Detecting>().enabled = true;
                    drone.GetComponent<Patrolling>().enabled = true;
                    drone.GetComponent<Shooting>().enabled = false;
                }
                break;

            case "Shooting":
                Debug.Log("Shooting");
                foreach (GameObject obj in enemies)
                {
                    Transform tmpDrone = obj.transform.GetChild(0);
                    GameObject drone = tmpDrone.gameObject;
                    drone.GetComponent<Detecting>().enabled = true;
                    drone.GetComponent<Patrolling>().enabled = false;
                    drone.GetComponent<Shooting>().enabled = true;
                }
                break;
        }
    }

    public static float Range
    {
        get { return range; }
        set { range = value; }
    }

    public static string State
    {
        get { return currentState; }
        set { currentState = value; }
    }

    public static float DetectingRange
    {
        get { return detectingRange; }
        set { detectingRange = value; }
    }
}

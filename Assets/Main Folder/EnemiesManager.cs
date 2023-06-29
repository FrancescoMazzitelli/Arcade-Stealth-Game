using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemiesManager : MonoBehaviour
{
    public static string currentState;
    public static string previousState;

    private GameObject enemiesContainer;
    private static List<GameObject> enemies ;
    private Dictionary<string, string> enemiesParams = new Dictionary<string, string>(); 
    public static float range;
    public static float detectionRange;
    private static GameObject player;
    private static GameObject laser;

    void Awake()
    {
        List<GameObject> enemiesT = new List<GameObject>();

        GameObject enemiesContainerT = GameObject.FindGameObjectWithTag("Enemies");
        enemiesContainer = enemiesContainerT;
        GameObject playerContainer = GameObject.FindGameObjectWithTag("Player");
        Transform tmpPlayer = playerContainer.transform.GetChild(0);
        GameObject playerT = tmpPlayer.gameObject;
        player = playerT;
        GameObject laserT = GameObject.FindGameObjectWithTag("Laser");
        laser = laserT;

        int childCount = enemiesContainer.transform.childCount;


        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = enemiesContainer.transform.GetChild(i);
            GameObject childObject = childTransform.gameObject;
            enemiesT.Add(childObject);
        }

        enemies = enemiesT;

        SendScripts();
    }

    void SendScripts()
    {
        foreach(GameObject obj in enemies)
        {
            Transform tmpDrone = obj.transform.GetChild(0);
            GameObject drone  = tmpDrone.gameObject;

            Detecting.Player = player.transform;
            Detecting.DetectionRange = DetectionRange;
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
        if (Detecting.DetectionRange == 0 || Patrolling.Range == 0)
        {
            Detecting.Player = player.transform;
            Detecting.DetectionRange = DetectionRange;

            Patrolling.Range = range;

            Shooting.Player = player.transform;
        }

        foreach (GameObject obj in enemies)
        {
            Transform tmpDrone = obj.transform.GetChild(0);
            GameObject drone = tmpDrone.gameObject;

            PreviousState = CurrentState;
            CurrentState = drone.GetComponent<Detecting>().State;

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

    public static float DetectionRange
    {
        get { return detectionRange; }
        set { detectionRange = value; }
    }
}

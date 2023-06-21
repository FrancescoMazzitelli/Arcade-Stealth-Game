using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    private GameObject enemiesContainer;
    private List<GameObject> enemies = new List<GameObject>();
    private Dictionary<string, string> enemiesParams = new Dictionary<string, string>(); 

    void Start()
    {
        GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();

        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].name.Equals("Enemies")) 
                enemiesContainer = gameObjects[i];
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
            Patrolling.Range = 10;
            drone.AddComponent<Patrolling>();
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}

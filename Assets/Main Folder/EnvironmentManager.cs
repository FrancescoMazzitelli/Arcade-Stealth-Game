using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public static GameObject environmentContainer;
    private static List<GameObject> lights = new List<GameObject>();
    public static float range;
    public static float intensity;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();

        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].name.Equals("Level Design"))
                Container = gameObjects[i];
        }

        lights = GetChildren(environmentContainer.transform);
        foreach(GameObject obj in lights)
        {
            Light lightComponent = obj.GetComponent<Light>();
            lightComponent.range = Range;
            lightComponent.intensity = Intensity;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GameObject Container
    {
        get { return environmentContainer; }
        set { environmentContainer = value; }
    }

    public List<GameObject> GetChildren(Transform parent)
    {
        int childCount = parent.childCount;
        int count;
        List<GameObject> children = new List<GameObject>();

        for (int i = 0; i < childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.name.Equals("Lights"))
            {
                count = child.childCount;
                for (int j = 0; j < count; j++)
                {
                    children.Add(child.GetChild(j).gameObject);
                }
            }
                
        }
        return children;
    }

    public static float Range
    {
        get { return range; }
        set { range = value; }
    }

    public static float Intensity
    {
        get { return intensity; }
        set { intensity = value; }
    }
}

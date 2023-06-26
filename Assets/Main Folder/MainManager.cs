using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    static readonly string textFile = @"Configs\config.txt";
    // Start is called before the first frame update
    void Start()
    {
        FileReader reader = new FileReader(textFile);
        
        ParametersStructure parameters = reader.readParams();
        Dictionary<string, string> enemiesProp = parameters.enemiesProp;
        Dictionary<string, string> environmentProp = parameters.environmentProp;
        Dictionary<string, string> mainCharacterProp = parameters.mainCharacterProp;

        EnemiesManager.Range = float.Parse(enemiesProp["range"]);
        EnemiesManager.DetectionRange = float.Parse(enemiesProp["detectingRange"]);

        EnvironmentManager.Range = float.Parse(environmentProp["lightRange"]);
        EnvironmentManager.Intensity = float.Parse(environmentProp["lightIntensity"]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

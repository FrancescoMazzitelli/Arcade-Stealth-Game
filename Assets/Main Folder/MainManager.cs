using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static Dictionary<string, string> enemiesProp;
    public static Dictionary<string, string> environmentProp;
    public static Dictionary<string, string> mainCharacterProp;
    static readonly string textFile = @"Configs\config.txt";

    // Start is called before the first frame update
    void Awake()
    {
        FileReader reader = new FileReader(textFile);
        
        ParametersStructure parameters = reader.readParams();

        enemiesProp = parameters.enemiesProp;
        environmentProp = parameters.environmentProp;
        mainCharacterProp = parameters.mainCharacterProp;

        EnemiesManager.Range = float.Parse(enemiesProp["range"]);
        EnemiesManager.DetectionRange = float.Parse(enemiesProp["detectingRange"]);

        EnvironmentManager.Range = float.Parse(environmentProp["lightRange"]);
        EnvironmentManager.Intensity = float.Parse(environmentProp["lightIntensity"]);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Dictionary<string, string> Player
    {
        get { return mainCharacterProp; }
        set { mainCharacterProp = value; }
    }
}

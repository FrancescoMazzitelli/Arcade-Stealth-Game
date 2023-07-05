using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFeature
{
    private string featureName;
    private MonoBehaviour featureScript;

    public EnemyFeature(string name, MonoBehaviour script)
    {
        featureName = name;
        featureScript = script;
    }

    public string FeatureName
    {
        get { return featureName; }
        set { featureName = value; }
    }

    public MonoBehaviour FeatureScript
    {
        get { return featureScript; }
        set { featureScript = value; }
    }
}



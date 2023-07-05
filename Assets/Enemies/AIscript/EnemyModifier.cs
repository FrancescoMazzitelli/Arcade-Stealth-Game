using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyModifier
{
    protected string name;
    private Dictionary<EnemyFeature, bool> scriptSelection;
    private bool enabled = false;

    public string GetName
    {
        get { return name; }
    }

    public bool Enabled
    {
        get { return enabled; }
        set { enabled = value; }
    }

    public Dictionary<EnemyFeature, bool> ScriptSelection
    {
        get { return scriptSelection; }
        set { scriptSelection = value; }
    }

    public EnemyModifier(string t_value, Dictionary<EnemyFeature, bool> scriptS)
    {
        name = t_value;
        scriptSelection = scriptS;
    }
}
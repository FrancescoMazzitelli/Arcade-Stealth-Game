using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.IO;
using System.Linq;

public class EnemyComponentManager
{
    private List<EnemyFeature> features;
    private List<EnemyModifier> modifiers;

    public EnemyComponentManager()
    {
        features = new List<EnemyFeature> ();
        modifiers = new List<EnemyModifier> ();
    }
    public void Bind()
    {
        //Creazione tabella
        Table table = new Table();
        table.InsertFromCSV("Assets/Enemies/AIscript/EnemiesStates.csv");

        //Features
        string[] featuresNames = table.GetHeader();
        FillFeatures(featuresNames);

        //Modifiers
        List<string> tmpMod = new List<string>();
        int rows = table.NumRows;
        for(int i = 0; i < rows; i++)
        {
            tmpMod = table.GetRow(i);
            FillModifiers(tmpMod);
        }        
    }

    public void Update()
    {
        
    }

    private void FillFeatures(string[] names)
    {
        List<MonoBehaviour> scripts = GetAllScriptTypes();
        EnemyFeature feature;

        foreach (string name in names)
        {
            foreach(MonoBehaviour script in scripts)
            {
                string scriptName = script.GetType().Name;
                if (scriptName.Trim().Equals(name.Trim()))
                {
                    feature = new EnemyFeature(scriptName, script);
                    if (!features.Contains(feature))
                        features.Add(feature);
                }
            }
        }
    }

    private void FillModifiers(List<string> mod)
    {
        string state = null;
        Dictionary<EnemyFeature, bool> flags = new Dictionary<EnemyFeature, bool>();

        for(int i = 0; i < mod.Count; i++)
        {
            if (!(mod[i].Trim().StartsWith("1") || mod[i].Trim().StartsWith("0")))
            {
                state = mod[i].Trim();
            }
            else
            {
                bool boolValue = (mod[i].Trim() == "1") ? true : false;
                int j = i - 1;
                flags.Add(features[j], boolValue);
            }
        }
        if(state != null && flags.Count != 0)
        {
            EnemyModifier modifier = new EnemyModifier(state, flags);
            modifiers.Add(modifier);
        }
        
    }

    private List<MonoBehaviour> GetAllScriptTypes()
    {
        GameObject gameObject = GameObject.Find("AiScripts");
        List<MonoBehaviour> scripts = new List<MonoBehaviour>();
        MonoBehaviour[] components = gameObject.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour component in components)
        {
            scripts.Add(component);
        }

        return scripts;
    }

    public List<EnemyFeature> Features
    {
        get { return features; }
        set { features = value; }
    }

    public List<EnemyModifier> Modifiers
    {
        get { return modifiers; }
        set { modifiers = value; }
    }
}

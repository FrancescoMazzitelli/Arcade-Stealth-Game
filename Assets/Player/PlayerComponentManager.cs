using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.IO;
using System.Linq;

public class PlayerComponentManager
{
    private List<PlayerFeature> features;
    private List<PlayerModifier> modifiers;
    private Dictionary<PlayerModifier, bool> activeModifiers;

    private static PlayerComponentManager instance;

    public static PlayerComponentManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerComponentManager();
            }
            return instance;
        }
    }

    private PlayerComponentManager()
    {
        features = new List<PlayerFeature>();
        modifiers = new List<PlayerModifier>();
        activeModifiers = new Dictionary<PlayerModifier, bool>();
        Bind();
    }

    public void Bind()
    {
        //Creazione tabelle
        Table table = new Table();
        table.InsertFromCSV("Assets/Player/Modifiers.csv");
        Table active = new Table();
        active.InsertFromCSV("Assets/Player/ActiveModifiers.csv");

        //Features
        Dictionary<string, string> playerBaseValues = MainManager.Player;
        FillFeatures(playerBaseValues);

        //Modifiers
        List<string> tmpMod = new List<string>();
        int rows = table.NumRows;
        for (int i = 0; i < rows; i++)
        {
            tmpMod = table.GetRow(i);
            FillModifiers(tmpMod);
        }

        //ActiveModifiers
        int rowsActive = active.NumRows;
        if(rowsActive > 0)
        {
            List<string> tmpActive = new List<string>();
            for (int i = 0; i < rowsActive; i++)
            {
                tmpActive = active.GetRow(i);
                FillActiveModifers(tmpActive);
            }
        }

        //Ripristina i modifier attivi per acquisti passati
        foreach (PlayerModifier modifier in modifiers)
        {
            foreach(KeyValuePair<PlayerModifier, bool> pair in activeModifiers)
            {
                PlayerModifier key = pair.Key;
                bool value = pair.Value;

                if (modifier.GetName.Equals(key.GetName)){
                    modifier.Enabled = value;
                }
            }
        }
    }

    public void FillFeatures(Dictionary<string, string> values)
    {
        foreach (KeyValuePair<string, string> pair in values)
        {
            string key = pair.Key.Trim();
            string value = pair.Value.Trim();
            float baseValue = float.Parse(value);

            PlayerFeature feature = new PlayerFeature(key, baseValue, baseValue);
            features.Add(feature);
        }
    }

    public void FillModifiers(List<string> mod)
    {
        string name = mod[0].Trim();
        string featureS = mod[1].Trim();
        string valueS = mod[2].Trim();
        float value = float.Parse(valueS);
        PlayerFeature feature = null;

        foreach(PlayerFeature x in features) 
        {
            if(x.Name == featureS)
                feature = x;
        }

        if(feature != null)
        {
            PlayerModifier modifier = new PlayerModifier(name, feature, value);
            modifiers.Add(modifier);
        }
    }

    public void FillActiveModifers(List<string> act)
    {
        string name = act[0].Trim();
        string value = act[1].Trim();

        bool boolValue = (value == "1") ? true : false;

        foreach (PlayerModifier modifier in modifiers)
        {
            if(modifier.GetName.Trim().Equals(name))
            {
                activeModifiers.Add(modifier, boolValue);
            }
        }
    }

    public List<PlayerFeature> Features
    {
        get { return features; }
        set { features = value; }
    }

    public List<PlayerModifier> Modifiers
    {
        get { return modifiers; }
        set { modifiers = value; }
    }

    public Dictionary<PlayerModifier, bool> ActiveModifiers
    {
        get { return activeModifiers; }
        set { activeModifiers = value; }
    }

}

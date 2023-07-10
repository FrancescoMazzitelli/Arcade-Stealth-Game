using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeature
{
    private string name;
    private float baseValue;
    private float currentValue;

    public PlayerFeature(string nameS, float value, float current)
    {
        name = nameS;
        baseValue = value;
        currentValue = current;
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public float BaseValue
    {
        get { return baseValue; }
        set { baseValue = value; }
    }

    public float CurrentValue
    {
        get { return currentValue; }
        set { currentValue = value; }
    }
}

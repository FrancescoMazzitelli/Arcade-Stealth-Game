using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModifier
{
    protected string name;
    private PlayerFeature feature;
    private float mult_factor = 1f; //default
    private float add_factor = 0f; //default
    private bool enabled = false; //default

    public string GetName
    {
        get { return name; }
    }

    public bool Enabled
    {
        get { return enabled; }
        set { enabled = value; }
    }

    public PlayerFeature Feature
    {
        get { return feature; }
        set { feature = value; }
    }

    public float MultFactor
    {
        get { return mult_factor; }
        set { mult_factor = value; }
    }

    public float AddFactor
    {
        get { return add_factor; }
        set { add_factor = value; }
    }

    public PlayerModifier(string t_name, PlayerFeature featureS, float a_factor)
    {
        name = t_name;
        feature = featureS;
        add_factor = a_factor;
    }

    public PlayerModifier(string t_name, PlayerFeature featureS, float a_factor, float m_factor)
    {
        name = t_name;
        feature = featureS;
        add_factor = a_factor;
        mult_factor = m_factor;
    }
}

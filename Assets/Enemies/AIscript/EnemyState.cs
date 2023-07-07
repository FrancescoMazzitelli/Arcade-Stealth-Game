using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyState
{
    private Dictionary<string, bool> toEnable;

    public Dictionary<string, bool> ToEnable
    {
        get { return toEnable; }
        set { toEnable = value; }
    }

    public EnemyState(Dictionary<string, bool> row)
    {
        toEnable = row;
    }
}
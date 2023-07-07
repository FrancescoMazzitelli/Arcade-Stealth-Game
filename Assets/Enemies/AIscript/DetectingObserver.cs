using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectingObserver
{
    private List<Detecting> subjects;
    public EnemyComponentManager manager;

    public DetectingObserver()
    {
        manager = EnemyComponentManager.Instance;
    }

    public void Bind(List<Detecting> s)
    {
        this.subjects = s;
        foreach(Detecting subject in subjects)
        {
            subject.detected += OnFlagChanged;
        }
    }

    private void OnFlagChanged(bool newFlag)
    {
        EnemyState state = FindState();
        bool isFirstElement = true;

        if (state != null)
        {
            foreach (KeyValuePair<string, bool> pair in state.ToEnable)
            {
                if (isFirstElement)
                {
                    isFirstElement = false;
                    continue;
                }

                foreach (EnemyModifier modifier in manager.Modifiers)
                {
                    string key = pair.Key;
                    bool value = pair.Value;

                    if (modifier.GetName.Equals(key))
                    {
                        modifier.Enabled = value;
                    }
                }
            }
        }
    }

    private EnemyState FindState()
    {
        foreach (EnemyState state in manager.States)
        {
            foreach (KeyValuePair<string, bool> pair in state.ToEnable)
            {
                string key = pair.Key;
                bool value = pair.Value;
                foreach(Detecting subject in subjects)
                {
                    if (key.Equals(subject.GetType().Name) && value == subject.State)
                    {
                        return state;

                    }
                }
                break;
            }
        }

        return null;
    }
}

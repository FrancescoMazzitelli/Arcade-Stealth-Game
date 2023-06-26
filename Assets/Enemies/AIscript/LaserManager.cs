using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    private static int currentHealth = 100;

    private void Start()
    {
        Collider collider = GetComponent<Collider>();

        // Imposta la proprietà "isTrigger" del collider su true
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            currentHealth = currentHealth - 5;
        }
    }

    public static int Health
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
}

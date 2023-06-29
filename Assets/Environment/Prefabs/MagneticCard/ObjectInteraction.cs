using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    private bool canInteract;
    private static bool interaction;
    private GUIStyle style;

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            interaction = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }

    private void OnGUI()
    {
        style = new GUIStyle(GUI.skin.label);
        style.fontSize = 18; // Dimensione del font
        style.normal.textColor = Color.black; // Colore del test

        if (canInteract)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "Premi E per interagire", style);
        }
    }

    public static bool Interaction
    {
        get { return interaction; }
        set { interaction = value; }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detecting : MonoBehaviour
{
    public static Transform player;
    public static float detectionRange;
    private static LayerMask playerLayer;
    public string state;

    // Start is called before the first frame update
    void Start()
    {
        playerLayer = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Se il giocatore è all'interno della distanza di rilevamento
        if (distanceToPlayer <= detectionRange)
        {
            // Genera un raycast per individuare il giocatore
            RaycastHit hit;
            if (Physics.Raycast(transform.position, player.position - transform.position, out hit, detectionRange, playerLayer))
            {
                // Verifica se il raycast ha colpito il giocatore
                if (hit.collider.CompareTag("Player"))
                {
                    // Il nemico ha individuato il giocatore
                    Debug.Log("Il nemico ha individuato il giocatore!");
                    state = "Shooting";
                }
            }
        }
        else
        {
            state = "Patrolling";
        }
    }

    public static Transform Player
    {
        get { return player; }
        set { player  = value; }
    }

    public static float DetectionRange
    {
        get { return detectionRange; }
        set { detectionRange = value; }
    }

    public string State
    {
        get { return state; }
        set { state = value; }
    }

}








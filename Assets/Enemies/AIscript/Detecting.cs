using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Detecting : MonoBehaviour
{
    public static Transform player;
    public static float detectionRange = 5f;
    private static LayerMask playerLayer;
    public bool rilevato;
    public event Action<bool> detected;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerContainer = GameObject.FindGameObjectWithTag("Player");
        player = playerContainer.transform.GetChild(0);

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
                    // Debug.Log("Il nemico ha individuato il giocatore!");
                    State = true;
                }
            }
        }
        else
        {
            State = false;
        }
    }

    private void NotifyObservers()
    {
        detected?.Invoke(rilevato);
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

    public bool State
    {
        get { return rilevato; }
        set
        {
            rilevato = value;
            NotifyObservers();
        }
    }

}
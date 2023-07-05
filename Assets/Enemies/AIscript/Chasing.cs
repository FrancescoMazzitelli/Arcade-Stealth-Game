using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public static Transform player;
    public float stoppingDistance = 2.3f; // Distanza di arresto rispetto al player

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        GameObject playerContainer = GameObject.FindGameObjectWithTag("Player");
        player = playerContainer.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
        if (Vector3.Distance(transform.position, player.position) <= stoppingDistance)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }

    }
}

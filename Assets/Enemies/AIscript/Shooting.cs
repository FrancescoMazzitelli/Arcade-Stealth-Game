using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public static Transform player;
    public GameObject laserPrefab;
    public Transform firePoint;
    public static float fireRate = 0.5f;
    public static float force = 25;
    public float stoppingDistance = 2.3f; // Distanza di arresto rispetto al player

    private float nextFireTime;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();

        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].name.Equals("Laser"))
                laserPrefab = gameObjects[i];
        }

        firePoint = agent.transform;
        nextFireTime = Time.time;
    }

    void Update()
    {
        // Imposta la destinazione del nemico al player
        agent.SetDestination(player.position);

        // Controlla se il nemico è vicino al player e ferma l'agente di navigazione
        if (Vector3.Distance(transform.position, player.position) <= stoppingDistance)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }

        if (Time.time >= nextFireTime)
        {
            FireLaser();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private void FireLaser()
    {
        GameObject laserObj = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        Rigidbody laserRig = laserObj.AddComponent<Rigidbody>();
        laserRig.AddForce(agent.transform.forward * force, ForceMode.Impulse);
        Destroy(laserObj, 2f);
    }

    public static Transform Player
    {
        get { return player; }
        set { player = value; }
    }
}

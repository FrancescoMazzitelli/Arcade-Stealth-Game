using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public static Transform player;
    public GameObject laserPrefab;       // Prefab del raggio laser
    public Transform firePoint;         // Punto di spawn del raggio laser
    public static float fireRate = 0.5f;         // Frequenza di sparo dei raggi laser (in secondi)
    public static float force = 25;

    private float nextFireTime;         // Tempo prossimo sparo

    // Start is called before the first frame update
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
        nextFireTime = Time.time;       // Inizializza il tempo prossimo sparo all'avvio dello script
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
        // Controlla se � il momento di sparare
        if (Time.time >= nextFireTime)
        {
            FireLaser();                 // Spara il raggio laser
            nextFireTime = Time.time + 1f / fireRate;   // Aggiorna il tempo prossimo sparo
        }
    }

    private void FireLaser()
    {
        // Spawn del raggio laser utilizzando il prefab e il punto di spawn
        GameObject laserObj = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        Rigidbody laserRig = laserObj.AddComponent<Rigidbody>();
        laserRig.AddForce(laserRig.transform.forward * force, ForceMode.Impulse);
        Destroy(laserObj, 2f);
    }

    public static Transform Player
    {
        get { return player; }
        set { player = value; }
    }
}

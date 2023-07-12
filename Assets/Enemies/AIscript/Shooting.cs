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

    private float nextFireTime;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        laserPrefab = GameObject.FindWithTag("Laser");

        GameObject playerContainer = GameObject.FindGameObjectWithTag("Player");
        player = playerContainer.transform.GetChild(0);

        firePoint = agent.transform;
        nextFireTime = Time.time;
    }

    void Update()
    {
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

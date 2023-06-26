using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class Patrolling : MonoBehaviour 
{
    public NavMeshAgent agent;
    private static float range; 
    private GameObject targetAgent;

    public void Initialize(GameObject target)
    {
        targetAgent = target;
    }

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    public void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(agent.transform.position, range, out point)) //pass in our centre point and radius of area
            {
                //Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }
        }

    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 10.0f, NavMesh.AllAreas)) 
        {
            //the 10.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    public static float Range
    {
        get { return range; }
        set { range = value; }
    }


}

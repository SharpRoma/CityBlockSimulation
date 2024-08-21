using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DudeMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public int currentWaypointIndex = 0;
    private NavMeshAgent aagent;

    void Start()
    {
        aagent = GetComponent<NavMeshAgent>();
        MoveToNextWaypoint();
    }

    void Update()
    {
        if (aagent.remainingDistance < aagent.stoppingDistance)
        {
            if (aagent.pathPending)
                return;
            MoveToNextWaypoint();
        }
    }

    void MoveToNextWaypoint()
    {
        if (currentWaypointIndex >= waypoints.Length)
        {
            currentWaypointIndex = 0;
        }

        aagent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex++;

        
    }
}

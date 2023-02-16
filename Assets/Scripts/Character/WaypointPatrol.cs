using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{

    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform[] waypoints;

    int m_CurrentWaypointIndex;
    void Start()
    {
        Patrol();
    }

    void Update()
    {
        if (CheckWaypointsExistance() && navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance) {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
        
    }

    private void OnDrawGizmos()
    {
        if (CheckWaypointsExistance())
        {
            Vector3 startPosition = waypoints[0].position;
            Vector3 previousPosition = startPosition;

            foreach (Transform waypoint in waypoints)
            {
                Gizmos.DrawSphere(waypoint.position, 0.25f);
                Gizmos.DrawLine(previousPosition, waypoint.position);
                previousPosition = waypoint.position;
            }
            Gizmos.DrawLine(previousPosition, startPosition);
        }
    }

    void Patrol()
    {
        if (CheckWaypointsExistance())
        {
            navMeshAgent.SetDestination(waypoints[0].position);
        }
    }

    private bool CheckWaypointsExistance()
    {
        return waypoints != null && waypoints.Length > 0 && waypoints[0] != null;
    }
}

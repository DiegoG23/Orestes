using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform[] waypoints;

    private int _currentWaypointIndex;

    public bool IsPatrolling { get; set; }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        if (CheckWaypointsExistance())
        {
            navMeshAgent.SetDestination(waypoints[0].position);
        }
    }

    void Update()
    {
        Patrol();
    }


    public void Patrol()
    {
        if (IsPatrolling && CheckWaypointsExistance() && navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance) {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[_currentWaypointIndex].position);
        }
    }

    private bool CheckWaypointsExistance()
    {
        return waypoints != null && waypoints.Length > 0 && waypoints[0] != null && navMeshAgent != null;
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

            if(navMeshAgent != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(navMeshAgent.destination, 0.25f);
            }
        }
    }
}

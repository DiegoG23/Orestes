using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waypoint
{

    [SerializeField] private Vector3 waypointPosition;
    [SerializeField] private bool hasRest;

    public Vector3 WaypointPosition { get => waypointPosition; set => waypointPosition = value; }
    public bool HasRest { get => hasRest; set => hasRest = value; }
}

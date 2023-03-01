using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waypoint
{
    [SerializeField] private bool isRest;
    [SerializeField] private Vector3 direction;
    
    public bool IsRest { get => isRest; set => isRest = value; }
    public Vector3 Direction { get => direction; set => direction = value; }

}

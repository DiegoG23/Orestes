using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupType : MonoBehaviour
{
    [SerializeField] protected Pickup prefab;
    [SerializeField] protected Player[] playersAbleToPickup;
}
using System;
using UnityEngine;


public abstract class Enemy : Character
{
    [SerializeField] protected Transform target;
    [SerializeField] protected float rotationSpeed = 10.0f;
    [SerializeField] protected float visionRange;
    [SerializeField] protected float viewConeAngle;
    [SerializeField] protected Light viewCone;

    public bool TargetOnSight { get; protected set; } = false;

    public abstract void Disable();

    private void Start()
    {
        target = GameManager.instance.Player.transform;
    }

    private void Update()
    {
    }

}
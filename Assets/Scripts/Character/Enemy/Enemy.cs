using UnityEngine;


public abstract class Enemy : Character, ISpinnable
{
    [SerializeField] protected Transform target;
    [SerializeField] protected float rotationSpeed = 10.0f;
    public bool TargetOnSight { get; private set; } = true;

    public float RotationSpeed { get => rotationSpeed; }
    public Transform Self { get => transform; }

    public void LookAtTarget(Vector3 target)
    {
        CharacterActions.LookAtTarget(this, target);
    }
}
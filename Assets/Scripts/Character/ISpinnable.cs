using UnityEngine;

public interface ISpinnable
{
    public float RotationSpeed { get; }
    public Transform Self { get; }

    public void LookAtTarget(Vector3 target);
}
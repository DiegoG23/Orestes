using UnityEngine;

public interface IMovable : ISpinnable
{
    public float FollowTargetOffset { get; }
    public float Speed { get; }
    public void MoveToTarget(Vector3 target);


}
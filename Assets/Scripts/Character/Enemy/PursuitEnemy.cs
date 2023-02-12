using UnityEngine;

public class PursuitEnemy : Enemy, IMovable
{

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float followTargetOffset = 2.0f;
    public float FollowTargetOffset { get => followTargetOffset; }
    public float Speed { get => speed; }


    void Update()
    {
        MoveToTarget(target.position);
    }

    public void MoveToTarget(Vector3 targetPosition)
    {
        CharacterActions.MoveToTarget(this, targetPosition);
    }

}

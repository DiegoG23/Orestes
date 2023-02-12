using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Vectores
{
    public class BossEnemy : Enemy, IShooter, IMovable
    {
        [SerializeField] private Transform gunPlaceholder;
        [SerializeField] private EnemyState state = EnemyState.ALERT;

        [SerializeField] private float followTargetOffset = 2.0f;
        [SerializeField] private float speed = 8.0f;

        public float FollowTargetOffset { get => followTargetOffset; }
        public float Speed { get => speed; }

        void Start()
        {
        }

        void Update()
        {
            HandleState();
            /*
            ToggleShoot(TargetOnSight);
            if (TargetOnSight)
            {
                LookAtPosition(Target);
                MoveToTarget(Target);
            }
            */
        }

        private void HandleState()
        {
            switch (state)
            {
                case EnemyState.SHOOTING:
                    LookAtTarget(target.position);
                    Shoot(target);
                    break;
                case EnemyState.ALERT:
                    LookAtTarget(target.position);
                    break;
                case EnemyState.PURSUIT:
                    MoveToTarget(target.position);
                    break;
                case EnemyState.IDLE:
                default:
                    DoNothing();
                    break;
            }
        }
        public void Shoot(Transform target)
        {
            CharacterActions.Shoot(this, target);
        }


        public void MoveToTarget(Vector3 target)
        {
            CharacterActions.MoveToTarget(this, target);
        }

        public void DoNothing()
        {
            Debug.Log("DOING NOTHING");
        }

        public void Shoot()
        {
            throw new NotImplementedException();
        }
    }
}
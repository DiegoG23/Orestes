using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Vectores
{
    public class BossEnemy : Enemy
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
        }

        private void HandleState()
        {
            switch (state)
            {
                case EnemyState.SHOOTING:
                    Shoot(target);
                    break;
                case EnemyState.ALERT:
                    break;
                case EnemyState.PURSUIT:
                    MoveToTarget(target.position);
                    break;
                case EnemyState.IDLE:
                default:
                    Patrol();
                    break;
            }
        }
        public void Shoot(Transform target)
        {

        }


        public void MoveToTarget(Vector3 target)
        {

        }

        public void Patrol()
        {
            
        }

        public override void Disable()
        {
            throw new NotImplementedException();
        }
    }
}
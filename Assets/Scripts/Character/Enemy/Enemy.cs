using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : Character
{
    [SerializeField] protected float rotationSpeed = 10.0f;
    [SerializeField] protected float visionRange = 17.0f;
    [SerializeField] protected float viewConeAngle = 38.0f;
    [SerializeField] protected Light viewCone;

    [SerializeField] protected EnemyState state = EnemyState.IDLE;

    private Vector3 playerLastPosition;
    protected Player player;


    public bool PlayerOnSight { get; protected set; } = false;
    public bool IsAlert { get; protected set; } = false;


    private void Start()
    {
        player = GameManager.instance.Player;
    }


    private void Update()
    {
        CheckViewcone();
        HandleState();

    }

    private void CheckViewcone()
    {
        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;
            Vector3 vectorToPlayer = playerPosition - transform.position;
            if (IsAlert)
            {

            }
            //player inside viewcone
            else if (Vector3.Distance(transform.position, playerPosition) <= visionRange && Vector3.Angle(transform.forward, vectorToPlayer) <= viewConeAngle)
            {
                LayerMask layerMask = LayerMask.GetMask("Obstacles");
                //player at sight
                if (!Physics.Raycast(transform.position, vectorToPlayer, visionRange, layerMask))
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    {
                        state = EnemyState.ALERT;
                    }
                    else if (animator.GetCurrentAnimatorStateInfo(0).IsName("DroneAlert") && state != EnemyState.PURSUIT)
                    {
                        state = EnemyState.SHOOTING;
                    }
                    else
                    {
                        state = EnemyState.PURSUIT;
                    }
                }
            }

        }

    }




    private void HandleState()
    {
        switch (state)
        {
            case EnemyState.SHOOTING:
                Shoot();
                break;
            case EnemyState.ALERT:
                Alert();
                break;
            case EnemyState.PURSUIT:
                Pursuit();
                break;
            case EnemyState.IDLE:
                Patrol();
                break;
            case EnemyState.DISABLED:
            default:
                break;
        }
    }
    public abstract void Disable();

    private void Pursuit()
    {
        //TODO implementar persecución
        Debug.Log("Pursuing");

        animator.SetBool("isAttacking", false);
        animator.SetBool("isAlert", true);

        agent.SetDestination(playerLastPosition);

    }


    private void Alert()
    {
        //TODO implementar alerta
        Debug.Log("Alert");

        animator.SetBool("isAttacking", false);
        animator.SetBool("isAlert", true);

        viewCone.color = Color.red;
    }


    private void Patrol()
    {
        //TODO implementar patrulla... analizar uso de waypointpatrol
        Debug.Log("Patrolling");

        animator.SetBool("isAttacking", false);
        animator.SetBool("isAlert", false);

        viewCone.color = Color.green;
    }


    private void Shoot()
    {
        Debug.Log("Shooting Player");

        animator.SetBool("isAttacking", true);

        player.Die();
    }


}
using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : Character
{
    [SerializeField] protected float rotationSpeed = 10.0f;
    [SerializeField] protected float visionRange = 10.6f;
    [Range(0,360)]
    [SerializeField] protected float viewConeAngle = 80.0f;
    [SerializeField] protected Light viewCone;
    [SerializeField] protected float pursuingMaxTime = 5.0f;
    [SerializeField] protected float shootingMaxTime = 2.0f;

    [SerializeField] protected Color VIEWCONE_COLOR_PATROL = Color.green;
    [SerializeField] protected Color VIEWCONE_COLOR_WARNING = Color.yellow;
    [SerializeField] protected Color VIEWCONE_COLOR_DANGER = Color.red;

    [SerializeField] protected EnemyState state = EnemyState.PATROL;

    protected WaypointPatrol _waypointPatrol;
    protected Player _player;

    private Vector3 _playerLastPosition;
    private float _currentPursuingTime = 0f;
    private float _currentShootingTime = 0f;



    public bool PlayerOnSight { get; protected set; } = false;
    public bool IsAlert { get; protected set; } = false;

    public float VisionRange { get => visionRange; set => visionRange = value; }
    public float ViewConeAngle { get => viewConeAngle; set => viewConeAngle = value; }

    protected override void Awake()
    {
        base.Awake();
        _waypointPatrol = GetComponent<WaypointPatrol>();
    }
    protected override void Start()
    {
        base.Start();
        _player = GameManager.instance.Player;
    }


    private void Update()
    {
        CheckViewcone();
        HandleState();
    }

    private void CheckViewcone()
    {
        PlayerOnSight = false;
        if (_player == null || _player.IsDead)
        {
            return;
        }

        Vector3 playerPosition = _player.transform.position;
        Vector3 vectorToPlayer = (playerPosition - transform.position).normalized;

        if (Vector3.Distance(transform.position, playerPosition) <= visionRange)
        {
            Debug.Log("IN RANGEE!!!");
        }
        if (Vector3.Angle(transform.forward, vectorToPlayer) <= viewConeAngle)
        {
            Debug.Log("IN ANGLE!!!");
        }
        //player inside viewcone
        if (Vector3.Distance(transform.position, playerPosition) <= visionRange && Vector3.Angle(transform.forward, vectorToPlayer) <= viewConeAngle/2f)
        {

            LayerMask layerMask = LayerMask.GetMask("Obstacles");
            //player at sight
            if (!Physics.Raycast(transform.position, vectorToPlayer, visionRange, layerMask))
            {
                _playerLastPosition = playerPosition;
                PlayerOnSight = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_player)
        {
            if (Vector3.Distance(transform.position, _player.transform.position) <= visionRange)
            {

                Vector3 vToPlayer = (_player.transform.position - transform.position).normalized;
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(_player.transform.position, 0.25f);
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(transform.forward, 0.25f);
                //Gizmos.color = Color.magenta;
                //Gizmos.DrawSphere(transform.position, 0.25f);
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(vToPlayer, 0.25f);
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(Vector3.zero, transform.forward);
                Gizmos.DrawLine(Vector3.zero, vToPlayer);
                Gizmos.color = Color.red;
                var rotation = Quaternion.AngleAxis(viewConeAngle/2f, Vector3.up);
                var forward = Vector3.forward;
                var right = rotation * forward;
                Gizmos.DrawLine(Vector3.zero, right);

            }
        }
    }


    private void HandleState()
    {
        switch (state)
        {
            case EnemyState.ALERT:
                Alert();
                break;
            case EnemyState.PURSUIT:
                Pursuit();
                break;
            case EnemyState.SHOOTING:
                Shoot();
                break;
            case EnemyState.PATROL:
                Patrol();
                break;
            case EnemyState.DISABLED:
            default:
                break;
        }
    }

    private void Alert()
    {
        Debug.Log("Alert");
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyAlert"))
        {
            state = EnemyState.PURSUIT;
            return;
        }

        _waypointPatrol.IsPatrolling = false;
        StopAgentOnPlace();

        viewCone.color = VIEWCONE_COLOR_WARNING;

        _animator.SetBool("isAttacking", false);
        _animator.SetBool("isAlert", true);


    }

    private void Pursuit()
    {
        Debug.Log("Pursuing for " + (pursuingMaxTime - _currentPursuingTime) + " seconds");
        if (PlayerOnSight)
        {
            state = EnemyState.SHOOTING;
            return;
        }
        if (_currentPursuingTime >= pursuingMaxTime)
        {
            state = EnemyState.PATROL;
            _currentPursuingTime = 0;
            return;
        }
        _agent.SetDestination(_playerLastPosition);
        _currentPursuingTime += Time.deltaTime;

        viewCone.color = VIEWCONE_COLOR_DANGER;

        _animator.SetBool("isAttacking", false);
        _animator.SetBool("isAlert", true);

    }



    private void Patrol()
    {
        Debug.Log("Patrolling");
        if (PlayerOnSight)
        {
            state = EnemyState.ALERT;
            return;
        }
        viewCone.color = VIEWCONE_COLOR_PATROL;

        _animator.SetBool("isAttacking", false);
        _animator.SetBool("isAlert", false);

        _waypointPatrol.IsPatrolling = true;
    }


    private void Shoot()
    {
        Debug.Log("Shooting Player");
        if (_player.IsDead)
        {
            _currentShootingTime += Time.deltaTime;
            if (_currentShootingTime > shootingMaxTime)
            {
                state = EnemyState.PATROL;
            }
            return;
        }
        StopAgentOnPlace();
        _player.Die();

        _animator.SetBool("isAttacking", true);
    }

    public virtual void Disable()
    {
        Debug.Log("Enemy Disabled");

        _agent.enabled = false;

        state = EnemyState.DISABLED;

        viewCone.gameObject.SetActive(false);

        _animator.SetBool("isDisabled", true);

    }


}
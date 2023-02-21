using System;
using System.Collections.Generic;
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

    private static Dictionary<GameObject, bool> enemiesDisabled = new Dictionary<GameObject, bool>();



    public bool PlayerOnSight { get; protected set; } = false;
    public bool IsAlert { get; protected set; } = false;

    public float VisionRange { get => visionRange; set => visionRange = value; }
    public float ViewConeAngle { get => viewConeAngle; set => viewConeAngle = value; }

    protected override void Awake()
    {
        base.Awake();
        _waypointPatrol = GetComponent<WaypointPatrol>();
        enemiesDisabled.Add(gameObject, false);
    }
    protected override void Start()
    {
        base.Start();
        _player = GameManager.instance.SelectedPlayer;
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

        //player inside viewcone (from viewcone light perspective)
        if (Vector3.Distance(transform.position, playerPosition) <= visionRange && Vector3.Angle(transform.forward, vectorToPlayer) <= viewConeAngle/2f)
        {

            LayerMask layerMask = LayerMask.GetMask("Obstacles");
            //player at sight (from enemy view perspective)
            if (!Physics.Raycast(transform.position, vectorToPlayer, visionRange, layerMask))
            {
                _playerLastPosition = playerPosition;
                PlayerOnSight = true;
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
        Debug.Log("Pursuing player " + GameManager.instance.SelectedPlayer.GetType() + ".");
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
        enemiesDisabled[gameObject] = true;

        _waypointPatrol.IsPatrolling = false;
        _agent.enabled = false;

        state = EnemyState.DISABLED;

        viewCone.gameObject.SetActive(false);

        _animator.SetBool("isDisabled", true);

    }


    public static bool AllEnemiesDisabled()
    {
        foreach (bool disabled in enemiesDisabled.Values)
        {
            if (!disabled)
            {
                return false;
            }
        }
        return true;
    }


}
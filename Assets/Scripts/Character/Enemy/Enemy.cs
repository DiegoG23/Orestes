using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : Character
{
    [SerializeField] protected float m_rotationSpeed = 10.0f;
    [SerializeField] protected float m_visionRange = 10.6f;
    [Range(0, 360)]
    [SerializeField] protected float m_viewConeAngle = 80.0f;
    [SerializeField] protected Light m_viewCone;
    [SerializeField] protected float m_pursuingMaxTime = 5.0f;
    [SerializeField] protected float m_shootingMaxTime = 2.0f;

    [SerializeField] protected Color VIEWCONE_COLOR_PATROL = Color.green;
    [SerializeField] protected Color VIEWCONE_COLOR_WARNING = Color.yellow;
    [SerializeField] protected Color VIEWCONE_COLOR_DANGER = Color.red;

    [SerializeField] protected EnemyState state = EnemyState.PATROL;

    protected WaypointPatrol m_waypointPatrol;

    private Vector3 m_targetLastPosition;
    private float m_currentPursuingTime = 0f;
    private float m_currentShootingTime = 0f;



    public Player PlayerAtSight { get; protected set; } = null;
    public bool IsAlert { get; protected set; } = false;

    public float VisionRange { get => m_visionRange; set => m_visionRange = value; }
    public float ViewConeAngle { get => m_viewConeAngle; set => m_viewConeAngle = value; }
    protected Player[] Players { get => GameManager.instance.Players; }

    protected override void Awake()
    {
        base.Awake();
        m_waypointPatrol = GetComponent<WaypointPatrol>();
    }


    private void Update()
    {
        CheckViewcone();
        HandleState();
    }

    private void CheckViewcone()
    {
        PlayerAtSight = null;
        foreach (Player player in Players)
        {
            if (player == null || player.IsDead)
            {
                break;
            }

            Vector3 l_playerPosition = player.transform.position;
            Vector3 l_vectorToPlayer = (l_playerPosition - transform.position).normalized;
            Vector3 l_viewConeVectorToPlayer = (l_playerPosition - transform.position).normalized;

            //player inside viewcone (from viewcone light perspective)
            if (Vector3.Distance(m_viewCone.transform.position, l_playerPosition) <= m_visionRange && Vector3.Angle(transform.forward, l_viewConeVectorToPlayer) <= m_viewConeAngle / 2f)
            {

                LayerMask layerMask = LayerMask.GetMask("Obstacles");
                //player at sight (from enemy view perspective), there's no obstacle between enemy and player
                if (!Physics.Raycast(transform.position, l_vectorToPlayer, m_visionRange, layerMask))
                {
                    if(PlayerAtSight == null)
                    {
                        m_targetLastPosition = l_playerPosition;
                        PlayerAtSight = player;
                    }
                }
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

        m_waypointPatrol.IsPatrolling = false;
        StopAgentOnPlace();

        m_viewCone.color = VIEWCONE_COLOR_WARNING;

        _animator.SetBool("isAttacking", false);
        _animator.SetBool("isAlert", true);


    }

    private void Pursuit()
    {
        if (PlayerAtSight != null)
        {
            Debug.Log("Pursuing player " + PlayerAtSight.GetType() + ".");
            state = EnemyState.SHOOTING;
            return;
        }
        if (m_currentPursuingTime >= m_pursuingMaxTime)
        {
            state = EnemyState.PATROL;
            m_currentPursuingTime = 0;
            return;
        }
        _agent.SetDestination(m_targetLastPosition);
        m_currentPursuingTime += Time.deltaTime;

        m_viewCone.color = VIEWCONE_COLOR_DANGER;

        _animator.SetBool("isAttacking", false);
        _animator.SetBool("isAlert", true);

    }



    private void Patrol()
    {
        if (PlayerAtSight != null)
        {
            state = EnemyState.ALERT;
            return;
        }
        m_viewCone.color = VIEWCONE_COLOR_PATROL;

        _animator.SetBool("isAttacking", false);
        _animator.SetBool("isAlert", false);

        m_waypointPatrol.IsPatrolling = true;
    }


    private void Shoot()
    {
        Debug.Log("Shooting Player");
        if (PlayerAtSight == null || PlayerAtSight.IsDead)
        {
            m_currentShootingTime += Time.deltaTime;
            if (m_currentShootingTime > m_shootingMaxTime)
            {
                state = EnemyState.PATROL;
            }
            return;
        }
        StopAgentOnPlace();
        PlayerAtSight.Die();

        _animator.SetBool("isAttacking", true);
    }

    public virtual void Disable()
    {
        Debug.Log("Enemy Disabled");

        m_waypointPatrol.IsPatrolling = false;
        _agent.enabled = false;

        state = EnemyState.DISABLED;

        m_viewCone.gameObject.SetActive(false);

        _animator.SetBool("isDisabled", true);

    }


}
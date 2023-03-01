using UnityEngine;
using UnityEngine.AI;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected float speed = 5.0f;
    [SerializeField] protected int health = 5;
    [SerializeField] protected int damage = 1;
    protected NavMeshAgent _agent;
    protected Animator _animator;

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }
    protected virtual void Start()
    {
        _agent.speed = speed;
    }

    protected void StopAgentOnPlace()
    {
        if (_agent.enabled)
        {
            _agent.destination = _agent.transform.position;
        }
    }
}
using UnityEngine;
using UnityEngine.AI;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected float speed = 5.0f;
    [SerializeField] protected int health = 5;
    [SerializeField] protected int damage = 1;
    protected NavMeshAgent agent;
    protected Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        agent.speed = speed;
    }
}
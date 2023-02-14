using UnityEngine;
using UnityEngine.AI;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected int health = 5;
    [SerializeField] protected int damage = 1;
    protected NavMeshAgent agent;
}
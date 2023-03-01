using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class Player : Character
{
    [SerializeField] protected float m_crouchedSpeed = 3.5f;
    [SerializeField] protected int m_initialDefaultCoreCharges = 1;

    protected bool m_isDead = false;
    protected bool m_isCrouched = false;
    protected bool m_isSelected = false;
    protected int m_defaultCoreCharges = 0;

    public bool IsDetected { get; protected set; }
    public bool IsDead { get => m_isDead; protected set => m_isDead = value; }
    public bool IsSelected { get => m_isSelected; set => m_isSelected = value; }


    protected override void Start()
    {
        base.Start();
        m_defaultCoreCharges = m_initialDefaultCoreCharges;
    }

    private void Update()
    {
        SyncAnimationWithAgent();
    }

    
    public void ToggleCrouch()
    {
        if (IsDead || !IsSelected)
        {
            Debug.Log($"Cannot crouch player {this.name}, they're dead or not selected.");
            return;
        }
        m_isCrouched = !m_isCrouched;
        _agent.speed = m_isCrouched ? m_crouchedSpeed : speed;
        StopAgentOnPlace();
        _animator.SetBool("isCrouched", m_isCrouched);
    }

    public void Die()
    {
        m_isDead = true;
        _animator.SetBool("isDead", m_isDead);
        _agent.enabled = false;
        GameManager.instance.LoseLevel();
    }



    public void MoveTo(Vector3 targetPosition)
    {
        if (IsDead || !IsSelected)
        {
            Debug.Log($"Cannot move player {this.name}, they're dead or not selected.");
            return;
        }
        _agent.SetDestination(targetPosition);
    }

    public void MoveTo(Pickup p_targetPickup)
    {
        if (!CanPickup(p_targetPickup))
        {
            //TODO Implementar Avisos
            Debug.Log($"Player {this.name} cannot get {p_targetPickup.name}");
            return;
        }
        //TODO agregar notificacion de estados para que el pickup sepa que el player va a buscarlo
        MoveTo(p_targetPickup.transform.position);
    }


    public virtual void Pickup(Pickup pickup)
    {
        StopAgentOnPlace();
        if (pickup is DefaultCorePickup)
        {
            m_defaultCoreCharges += pickup.Charges;
            return;
        }
    }

    public abstract void TriggerAbilityOne();
    public abstract void TriggerAbilityTwo();
    public abstract void TriggerAbilityThree();


    public virtual bool CanPickup(Pickup p_pickup)
    {
        return p_pickup is DefaultCorePickup;
    }


    private void SyncAnimationWithAgent()
    {
        if (_agent.enabled)
        {
            float velocity = _agent.isStopped ? 0 : _agent.velocity.magnitude;
            _animator.SetFloat("speed", velocity);
        }
    }

}
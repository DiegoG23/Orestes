using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class Player : Character
{
    [SerializeField] protected float crouchedSpeed = 3.5f;
    //KeyCodes
    //[SerializeField] protected KeyCode attackKeyCode = KeyCode.Space;
    [SerializeField] protected KeyCode crouchKeyCode = KeyCode.C;

    protected bool _isDead = false;
    protected bool _isCrouched = false;
    protected bool _isSelected = false;

    public bool IsDetected { get; protected set; }
    public bool IsDead { get => _isDead; protected set => _isDead = value; }


    protected virtual void Update()
    {
        if (!IsDead)
        {
            InputHandler();
        }
    }


    void InputHandler()
    {
        MovementHandler();
        CrouchHandler();
    }

    protected void CrouchHandler()
    {
        if (Input.GetKeyDown(crouchKeyCode))
        {
            _isCrouched = !_isCrouched;
            _agent.speed = _isCrouched ? crouchedSpeed : speed;
            StopAgentOnPlace();
            _animator.SetBool("isCrouched", _isCrouched);
        }
    }



    internal void Die()
    {
        _isDead = true;
        _animator.SetBool("isDead", _isDead);
        _agent.enabled = false;
        GameManager.instance.LoseLevel();
    }


    protected void MovementHandler()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            MoveToClickPoint();
        }
        */

        float velocity = _agent.isStopped ? 0 : _agent.velocity.magnitude;
        _animator.SetFloat("speed", velocity);
    }


    public void MoveToClickPoint()
    {
        Debug.Log("Moving Player!!!");
        RaycastHit hit;
        LayerMask layerMask = LayerMask.GetMask("PlayerNav", "Pickup");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.layer == LayerMask.NameToLayer("PlayerNav"))
            {
                Debug.Log("PLAYER NAV HIT!!!!!");

                // The raycast hit an object on the "PlayerNav" layer.
                // Do something here.
                _agent.SetDestination(hit.point);
            }
            else if (hitObject.layer == LayerMask.NameToLayer("Pickup"))
            {
                // The raycast hit an object on the "Pickup" layer.
                // Do something here.
                var pickup = hitObject.GetComponent<Pickup>();
                Debug.Log("PICKUP HIT!!!!!");
                _agent.SetDestination(hit.point);
            }
        }
    }

}
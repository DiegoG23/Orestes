using System;
using UnityEngine;
using UnityEngine.AI;

public class Player : Character
{
    [SerializeField] private float crouchedSpeed = 3.5f;
    [SerializeField] private float dashLength = 5.0f;
    //KeyCodes
    //[SerializeField] private KeyCode fireKeyCode = KeyCode.Space;
    [SerializeField] private KeyCode crouchKeyCode = KeyCode.C;
    [SerializeField] private KeyCode dashKeyCode = KeyCode.F;
    [SerializeField] private KeyCode pulseKeyCode = KeyCode.D;
    //Cooldowns
    [SerializeField] private float pulseCooldown = 5f;
    [SerializeField] private float dashCooldown = 5f;

    //Prefabs
    [SerializeField] private GameObject pulsePrefab;

    private float _pulseCooldownEndTime = 0;
    private float _dashCooldownEndTime = 0;
    private bool _isDead = false;
    private bool _isCrouched = false;

    public bool IsDetected { get; private set; }
    public bool IsDead { get => _isDead; private set => _isDead = value; }


    void Update()
    {
        InputHandler();
    }


    void InputHandler()
    {
        if (!IsDead)
        {
            MovementHandler();
            CrouchHandler();
            DashHandler();
            PulseHandler();
        }
    }

    private void CrouchHandler()
    {
        if (Input.GetKeyDown(crouchKeyCode))
        {
            _isCrouched = !_isCrouched;
            _agent.speed = _isCrouched ? crouchedSpeed : speed;
            StopAgentOnPlace();
            _animator.SetBool("isCrouched", _isCrouched);
        }
    }


    private void PulseHandler()
    {
        if (Input.GetKeyDown(pulseKeyCode) && _pulseCooldownEndTime <= Time.time)
        {
            _pulseCooldownEndTime = Time.time + pulseCooldown;
            StopAgentOnPlace();
            GameObject pulse = Instantiate(pulsePrefab, transform.position, Quaternion.identity, _animator.transform);
            Destroy(pulse, pulseCooldown);
        }
    }

    internal void Die()
    {
        _isDead = true;
        _animator.SetBool("isDead", _isDead);
        _agent.enabled = false;
        GameManager.instance.LoseLevel();
    }

    private void DashHandler()
    {

        if (Input.GetKeyDown(pulseKeyCode) && _pulseCooldownEndTime <= Time.time)
        {
            _pulseCooldownEndTime = Time.time + pulseCooldown;
            StopAgentOnPlace();
            GameObject pulse = Instantiate(pulsePrefab, transform.position, Quaternion.identity, _animator.transform);
            Destroy(pulse, pulseCooldown);
        }

        if (Input.GetKeyDown(dashKeyCode) && _dashCooldownEndTime <= Time.time)
        {
            _dashCooldownEndTime = Time.time + dashCooldown;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask layerMask = LayerMask.GetMask("PlayerNav");
            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                Vector3 playerToHitVector = hit.point - transform.position;
                Vector3 newPosition = hit.point;
                if (playerToHitVector.magnitude > dashLength)
                {
                    newPosition = transform.position + playerToHitVector.normalized * dashLength;
                }
                Debug.Log("ray hit: " + hit.point.ToString());
                Debug.Log("newPos: " + newPosition.ToString());
                _agent.Warp(newPosition);
            }
        }
    }

    private void MovementHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToClickPoint();
        }

        float velocity = _agent.isStopped ? 0 : _agent.velocity.magnitude;
        _animator.SetFloat("speed", velocity);
    }


    private void MoveToClickPoint()
    {
        Debug.Log("Moving Player!!!");
        RaycastHit hit;
        LayerMask layerMask = LayerMask.GetMask("PlayerNav");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            _agent.destination = hit.point;
        }
    }

}
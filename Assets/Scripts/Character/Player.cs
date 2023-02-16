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
    //Delays
    [SerializeField] private float pulseDelay = 5f;
    [SerializeField] private float dashDelay = 5f;

    //Prefabs
    [SerializeField] private GameObject pulsePrefab;

    private bool isDead = false;
    private float nextPulseAvailableTime = 0;
    private bool isCrouched = false;
    public bool IsDetected { get; private set; }
    public bool IsDead { get => isDead; private set => isDead = value; }

    void Start()
    {
        agent.speed = speed;
    }


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
            isCrouched = !isCrouched;
            agent.speed = isCrouched ? crouchedSpeed : speed;
            StopAgentOnPlace();
            animator.SetBool("isCrouched", isCrouched);
        }
    }


    private void PulseHandler()
    {
        if (Input.GetKeyDown(pulseKeyCode) && nextPulseAvailableTime <= Time.time)
        {
            nextPulseAvailableTime = Time.time + pulseDelay;
            StopAgentOnPlace();
            GameObject pulse = Instantiate(pulsePrefab, transform.position, Quaternion.identity, animator.transform);
            Destroy(pulse, pulseDelay);
        }
    }

    internal void Die()
    {
        isDead = true;
        animator.SetBool("isDead", isDead);
        GameManager.instance.LoseLevel();
    }

    private void DashHandler()
    {
        if (Input.GetKeyDown(dashKeyCode))
        {
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
                agent.Warp(newPosition);
            }
        }
    }

    private void MovementHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToClickPoint();
        }

        float velocity = agent.isStopped ? 0 : agent.velocity.magnitude;
        animator.SetFloat("speed", velocity);
    }


    private void MoveToClickPoint()
    {
        Debug.Log("Moving Player!!!");
        RaycastHit hit;
        LayerMask layerMask = LayerMask.GetMask("PlayerNav");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            agent.destination = hit.point;
        }
    }

    private void StopAgentOnPlace()
    {
        agent.destination = agent.transform.position;
    }
}
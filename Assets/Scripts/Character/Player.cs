using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Character
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float crouchedSpeed = 3.5f;
    [SerializeField] private float dashLength = 5.0f;
    [SerializeField] private KeyCode fireKeyCode = KeyCode.Space;
    [SerializeField] private KeyCode crouchKeyCode = KeyCode.C;
    [SerializeField] private KeyCode dashKeyCode = KeyCode.F;
    [SerializeField] private string vAxisName = "Vertical";
    [SerializeField] private Animator playerAnimator;

    private NavMeshAgent agent;

    private bool isCrouched = false;

    public bool IsDetected { get; private set; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        agent.speed = speed;
    }


    void Update()
    {
        InputHandler();
    }

    private void LateUpdate()
    {
        if (!agent.isStopped)
        {
            playerAnimator.SetFloat("speed", agent.velocity.magnitude);
        }
    }

    void InputHandler()
    {
        MovementHandler();
        CrouchHandler();
        DashHandler();
    }

    private void CrouchHandler()
    {
        if (Input.GetKeyDown(crouchKeyCode))
        {
            isCrouched = !isCrouched;
            agent.speed = isCrouched ? crouchedSpeed : speed;
            StopAgentOnPlace();
            playerAnimator.SetBool("isCrouched", isCrouched);
        }
    }

    private void DashHandler()
    {
        if (Input.GetKeyDown(dashKeyCode))
        {
            /*
            float vAxis = Input.GetAxis(vAxisName);
            float translation = vAxis + dashLength * Mathf.Sign(vAxis);
            */

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask layerMask = LayerMask.GetMask("PlayerNav");
            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                Debug.Log("ray hit: " + hit.point.ToString());
                agent.Warp(hit.point);
                NavMeshHit navMeshHit;
                if (NavMesh.SamplePosition(hit.point, out navMeshHit, 100f, layerMask))
                {
                    Debug.Log("agent warped: " + navMeshHit.position.ToString());
                    agent.Warp(navMeshHit.position);
                }
            }
        }
    }

    private void MovementHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToClickPoint();
        }
    }


    private void MoveToClickPoint()
    {
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
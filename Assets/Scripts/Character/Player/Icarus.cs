using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icarus: Player
{
    [SerializeField] protected float dashLength = 5.0f;

    //KeyCodes
    [SerializeField] protected KeyCode dashKeyCode = KeyCode.F;


    //Cooldowns
    [SerializeField] protected float dashCooldown = 5f;


    protected float _dashCooldownEndTime = 0;


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        DashHandler();
    }



    protected void DashHandler()
    {
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
}

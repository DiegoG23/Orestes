using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icarus: Player
{
    [SerializeField] protected float m_warpLength = 5.0f;

    [SerializeField] protected int m_initialWarpCoreCharges = 1;

    //Cooldowns
    [SerializeField] protected float m_warpCooldown = 5f;

    protected float m_warpCooldownEndTime = 0;
    protected int m_warpCoreCharges = 0;

    public int WarpCoreCharges { get => m_warpCoreCharges; }


    protected override void Start()
    {
        base.Start();
        m_warpCoreCharges = m_initialWarpCoreCharges;
    }

    private void TriggerWarp()
    {
        if (m_warpCoreCharges > 0 && m_warpCooldownEndTime <= Time.time)
        {
            m_warpCoreCharges--;
            m_warpCooldownEndTime = Time.time + m_warpCooldown;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask layerMask = LayerMask.GetMask("PlayerNav");
            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                Vector3 playerToHitVector = hit.point - transform.position;
                Vector3 newPosition = hit.point;
                if (playerToHitVector.magnitude > m_warpLength)
                {
                    newPosition = transform.position + playerToHitVector.normalized * m_warpLength;
                }
                Debug.Log("ray hit: " + hit.point.ToString());
                Debug.Log("newPos: " + newPosition.ToString());
                _agent.Warp(newPosition);
            }
        }
    }

    public override bool CanPickup(Pickup p_pickup)
    {
        return base.CanPickup(p_pickup) || p_pickup is WarpCorePickup;
    }

    public override void Pickup(Pickup pickup)
    {
        base.Pickup(pickup);
        if (pickup is WarpCorePickup)
        {
            m_warpCoreCharges += pickup.Charges;
            return;
        }
    }

    public override void TriggerAbilityOne()
    {
        TriggerWarp();
    }

    public override void TriggerAbilityTwo()
    {
        throw new System.NotImplementedException();
    }

    public override void TriggerAbilityThree()
    {
        throw new System.NotImplementedException();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electra : Player
{
    //Cooldowns
    [SerializeField] private float zapperCooldown = 5f;

    [SerializeField] protected int m_initialZapperCoreCharges = 1;

    //Prefabs
    [SerializeField] private Zapper zapperPrefab;

    private float _zapperCooldownEndTime = 0;
    private int m_zapperCoreCharges = 0;

    public int ZapperCoreCharges { get => m_zapperCoreCharges; }

    protected override void Start()
    {
        base.Start();
        m_zapperCoreCharges = m_initialZapperCoreCharges;
    }


    // ABILITIES
    private void TriggerZapper()
    {
        if(m_zapperCoreCharges > 0 && _zapperCooldownEndTime <= Time.time)
        {
            m_zapperCoreCharges--;
            _zapperCooldownEndTime = Time.time + zapperCooldown;
            StopAgentOnPlace();
            Instantiate(zapperPrefab, transform.position, Quaternion.identity, transform);
        }
    }


    // PICKUPS
    public override bool CanPickup(Pickup p_pickup)
    {
        return base.CanPickup(p_pickup) || p_pickup is ZapperCorePickup;
    }

    public override void Pickup(Pickup pickup)
    {
        base.Pickup(pickup);
        if (pickup is ZapperCorePickup)
        {
            m_zapperCoreCharges += pickup.Charges;
            return;
        }
    }

    public override void TriggerAbilityOne()
    {
        TriggerZapper();
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

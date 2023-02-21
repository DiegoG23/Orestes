using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electra : Player
{
    //KeyCodes
    [SerializeField] private KeyCode pulseKeyCode = KeyCode.D;

    //Cooldowns
    [SerializeField] private float pulseCooldown = 5f;

    //Prefabs
    [SerializeField] private GameObject pulsePrefab;

    private float _pulseCooldownEndTime = 0;



    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        PulseHandler();
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
}

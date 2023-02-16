using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Enemy
{
    public override void Disable()
    {
        state = EnemyState.DISABLED;
        viewCone.gameObject.SetActive(false);
        animator.SetBool("isDisabled", true);
    }
}

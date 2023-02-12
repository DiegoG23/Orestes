using UnityEngine;


public class StationaryEnemy : Enemy, IShooter
{
    void Start()
    {
    }


    void Update()
    {
        //ToggleShoot(TargetOnSight);
        if (TargetOnSight)
        {
            LookAtTarget(target.position);
        }
    }

    public void Shoot()
    {
        CharacterActions.Shoot(this, target);
    }

}
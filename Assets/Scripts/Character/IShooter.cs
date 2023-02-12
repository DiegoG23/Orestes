using UnityEngine;

public interface IShooter
{
    public Transform Self { get; }

    public void Shoot();

}
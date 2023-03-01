using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] protected int m_charges = 1;

    protected string m_pickupName;

    public int Charges { get => m_charges; protected set => m_charges = value; }

    protected void OnTriggerEnter(Collider collider)
    {
        GameObject other = collider.gameObject;
        if (other != null
            && other.CompareTag("Player")
            && other.TryGetComponent<Player>(out var player)
            && player.CanPickup(this))
        {
            player.Pickup(this);
            Destroy(gameObject);   
        }
    }

}

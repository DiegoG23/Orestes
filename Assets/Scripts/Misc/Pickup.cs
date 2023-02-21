using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private PickupType pickupType;
    [SerializeField] protected Player[] playersAbleToPickup;
    private List<Player> playersGoingToPickup = new List<Player>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsPlayerAbleToPickup()
    {
        var _playerType = GameManager.instance.SelectedPlayer.GetType();
        foreach (Player player in playersAbleToPickup)
        {
            if (player.GetType() == _playerType) //player found
            {
                return true;
            }
        }
        return false;
    }
}

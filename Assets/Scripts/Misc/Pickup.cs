using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private PickupType m_pickupType;
    [SerializeField] protected Player[] m_playersAbleToPickup;
    private List<Player> m_playersGoingToPickup = new List<Player>();


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
        var l_playerType = GameManager.instance.SelectedPlayer.GetType();
        foreach (Player player in m_playersAbleToPickup)
        {
            if (player.GetType() == l_playerType) //player found
            {
                return true;
            }
        }
        return false;
    }

    public void PlayerGoingToPickup(Player l_player)
    {
        m_playersGoingToPickup.Add(l_player);
    }

    public void PickedupByPlayer(Player l_player)
    {
        m_playersGoingToPickup.Remove(l_player);
    }
}

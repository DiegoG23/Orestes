using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionController : MonoBehaviour
{
    private LevelController m_LevelController;

    public static PlayerSelectionController instance;
    public bool dontDestroyOnLoad;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        m_LevelController = LevelController.instance;

        SelectPlayer(0);
        //m_LevelController.Camera.Follow = m_LevelController.Players[0].transform;
    }

    // TODO Optimizar creando una colección que mantenga los players seleccionados dentro del controlador
    public List<Player> GetSelectedPlayers()
    {
        List<Player> result = new List<Player>();

        foreach (Player player in m_LevelController.Players)
        {
            if (player.IsSelected && !player.IsDead)
            {
                result.Add(player);
            }
        }
        return result;
    }


    public void SwitchSelectedPlayer()
    {
        Player l_nextSelectedPlayer = null;
        int l_playersLength = m_LevelController.Players.Length;
        // deselecciono todos y me guardo la referencia al que tengo que seleccionar (siguiente al primer seleccionado actual)
        for (int i = 0; i < l_playersLength; i++)
        {
            if (l_nextSelectedPlayer == null && m_LevelController.Players[i].IsSelected)
            {
                l_nextSelectedPlayer = m_LevelController.Players[(i + 1) % l_playersLength];
            }
            m_LevelController.Players[i].IsSelected = false;
        }
        l_nextSelectedPlayer.IsSelected = true;
        m_LevelController.Camera.Follow = l_nextSelectedPlayer.transform;
    }

    public Player GetSelectedPlayer()
    {
        var selectedPlayers = GetSelectedPlayers();
        if (selectedPlayers.Count == 1)
        {
            return selectedPlayers[0];
        }
        return null;
    }

    public void SelectPlayer(int p_playerIndex)
    {
        if (p_playerIndex < 0 || p_playerIndex >= m_LevelController.Players.Length)
        {
            Debug.LogError("Invalid player index!");
            return;
        }
        Player l_player = m_LevelController.Players[p_playerIndex];
        foreach (Player player in m_LevelController.Players)
        {
            player.IsSelected = (player == l_player);
        }
        // TODO Solucionar que la cámara siga al mouse y no al jugador
        //m_camera.Follow = l_player.transform;
    }


    //Cuando se selecciona/deselecciona un player con Ctrl presionado
    public void ToggleSelectedPlayer(int p_playerIndex)
    {
        if (p_playerIndex < 0 || p_playerIndex >= m_LevelController.Players.Length)
        {
            Debug.LogError("Invalid player index!");
            return;
        }
        Player l_player = m_LevelController.Players[p_playerIndex];

        l_player.IsSelected = !l_player.IsSelected;

        // Si me quedo sin players seleccionados, no permito deseleccionar el player (lo vuelvo a IsSelected true)
        if (GetSelectedPlayers().Count < 1)
        {
            l_player.IsSelected = true;
        }
        // TODO Solucionar que la cámara siga al mouse y no al jugador
        //m_camera.Follow = l_player.transform;
    }

}

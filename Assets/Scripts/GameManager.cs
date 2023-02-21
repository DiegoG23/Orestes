using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player[] _players;
    [SerializeField] private CinemachineVirtualCamera _camera;

    [SerializeField] private KeyCode _switchPlayerKeyCode = KeyCode.Tab;
    [SerializeField] private KeyCode _saveLevelKeyCode = KeyCode.S;
    [SerializeField] private KeyCode _loadLevelKeyCode = KeyCode.L;
    [SerializeField] private KeyCode _restartLevelKeyCode = KeyCode.R;


    private int _selectedPlayerIndex = 0;


    public static GameManager instance;
    public bool dontDestroyOnLoad;

    public Player SelectedPlayer
    {
        get => _players[_selectedPlayerIndex];
        private set => SetSelectedPlayer(value); //TODO Borrar si no se llama de afuera, usar el field
    }

    private void SetSelectedPlayer(Player value)
    {
        var player = value;
        for (int i = 0; i < _players.Length; i++)
        {
            if (_players[i] == player)
            {
                _selectedPlayerIndex = i;
                _camera.Follow = player.transform;
                return;
            }
        }
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        SetSelectedPlayer(_players[_selectedPlayerIndex]);
    }

    private void Update()
    {
        LevelLoadHandler();
        SwitchPlayerHandler();
        ClickHandler();
    }

    private void LevelLoadHandler()
    {

        if (Input.GetKeyDown(_saveLevelKeyCode))
        {
            SaveLevel();
        }
        else if (Input.GetKeyDown(_loadLevelKeyCode))
        {
            LoadLevel();
        }
        else if (Input.GetKeyDown(_restartLevelKeyCode))
        {
            RestartLevel();
        }
    }

    private void RestartLevel()
    {
        throw new NotImplementedException();
    }

    private void LoadLevel()
    {
        throw new NotImplementedException();
    }

    private void SaveLevel()
    {
        throw new NotImplementedException();
    }

    private void SwitchPlayerHandler()
    {
        if (Input.GetKeyDown(_switchPlayerKeyCode))
        {
            _selectedPlayerIndex = (_selectedPlayerIndex + 1) % _players.Length;
            SetSelectedPlayer(_players[_selectedPlayerIndex]);
        }
    }

    public void ClickHandler()
    {

        if (Input.GetMouseButtonDown(0))
        {
            SelectedPlayer.MoveToClickPoint();
        }
    }

    public void WinLevel()
    {
        Debug.Log("YOU WIN!!!!");
    }

    public void LoseLevel()
    {
        Debug.Log("YOU LOSE!!!!");
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player[] m_players;
    [SerializeField] private CinemachineVirtualCamera _camera;

    [SerializeField] private KeyCode m_switchPlayerKeyCode = KeyCode.Tab;
    [SerializeField] private KeyCode m_saveLevelKeyCode = KeyCode.S;
    [SerializeField] private KeyCode m_loadLevelKeyCode = KeyCode.L;
    [SerializeField] private KeyCode m_restartLevelKeyCode = KeyCode.R;
    [SerializeField] private DifficultyLevel difficultyLevel = DifficultyLevel.HARCORE;



    private int m_selectedPlayerIndex = 0;

    public static GameManager instance;
    public bool dontDestroyOnLoad;

    public Player SelectedPlayer
    {
        get => m_players[m_selectedPlayerIndex];
        private set => SetSelectedPlayer(value);
    }
    public DifficultyLevel DifficultyLevel { get => difficultyLevel; private set => difficultyLevel = value; }

    private void SetSelectedPlayer(Player l_player)
    {
        for (int i = 0; i < m_players.Length; i++)
        {
            if (m_players[i] == l_player)
            {
                m_selectedPlayerIndex = i;
                _camera.Follow = l_player.transform;
                return;
            }
        }
    }

    public void RemovePlayer(Player l_player)
    {
        for (int i = m_players.Length - 1; i >= 0; i--)
        {
            if (m_players[i] == l_player)
            {
                m_selectedPlayerIndex = i;
                _camera.Follow = l_player.transform;
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
        SetSelectedPlayer(m_players[m_selectedPlayerIndex]);
    }

    private void Update()
    {
        LevelLoadHandler();
        SwitchPlayerHandler();
        ClickHandler();
    }

    private void LevelLoadHandler()
    {

        if (Input.GetKeyDown(m_saveLevelKeyCode))
        {
            SaveLevel();
        }
        else if (Input.GetKeyDown(m_loadLevelKeyCode))
        {
            LoadLevel();
        }
        else if (Input.GetKeyDown(m_restartLevelKeyCode))
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
        if (Input.GetKeyDown(m_switchPlayerKeyCode))
        {
            m_selectedPlayerIndex = (m_selectedPlayerIndex + 1) % m_players.Length;
            SetSelectedPlayer(m_players[m_selectedPlayerIndex]);
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
        if (Enemy.AllEnemiesDisabled())
        {
            Debug.Log("YOU DISABLED ALL ENEMIES!!!!");
        }
    }

    public void LoseLevel()
    {
        Debug.Log("YOU LOSE!!!!");
    }
}

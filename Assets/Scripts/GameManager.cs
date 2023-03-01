using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player[] m_players;
    [SerializeField] private Enemy[] m_enemies;
    [SerializeField] private CinemachineVirtualCamera m_camera;


    public static GameManager instance;
    public bool dontDestroyOnLoad;

    public Player[] Players { get => m_players; }
    public CinemachineVirtualCamera Camera { get => m_camera; }

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
        
    }

    private void Update()
    {

    }



    public void RestartLevel()
    {
        throw new NotImplementedException();
    }

    public void LoadLevel()
    {
        throw new NotImplementedException();
    }

    public void SaveLevel()
    {
        throw new NotImplementedException();
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

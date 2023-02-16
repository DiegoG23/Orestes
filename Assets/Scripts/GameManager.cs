using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;

    public static GameManager instance;
    public bool dontDestroyOnLoad;

    public Player Player { get => player; private set => player = value; }

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

    internal void WinLevel()
    {
        Debug.Log("YOU WIN!!!!");
    }

    internal void LoseLevel()
    {
        Debug.Log("YOU LOSE!!!!");
    }
}

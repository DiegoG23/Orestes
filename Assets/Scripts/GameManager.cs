using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Player Player { get; private set; }

    public static GameManager instance;
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
        Debug.Log("COLISION CON FINISH ZONE");
        Debug.Log("YOU WIN!!!!");
    }
}

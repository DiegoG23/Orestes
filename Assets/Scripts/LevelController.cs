using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    [SerializeField] private Player[] m_players;
    [SerializeField] private Enemy[] m_enemies;
    [SerializeField] private CinemachineVirtualCamera m_camera;
    [SerializeField] GameObject m_levelMainMenu;


    public Player[] Players { get => m_players; }
    public CinemachineVirtualCamera Camera { get => m_camera; }


    public static LevelController instance;
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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

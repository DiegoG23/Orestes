using Cinemachine;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private GameManager m_gameManager;
    private PlayerInputController m_playerInputController;
    private LevelUIController m_levelUIController;
    private PlayerSelectionController m_playerSelectionController;

    [SerializeField] private int levelNumber;
    [SerializeField] private string levelName;

    [SerializeField] private Player[] m_players;
    [SerializeField] private Enemy[] m_enemies;
    [SerializeField] private CinemachineVirtualCamera m_camera;
    [SerializeField] private GameObject m_levelMainMenu;



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
        }
        else
        {
            Destroy(gameObject);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = GameManager.instance;
        m_playerSelectionController = PlayerSelectionController.instance;
        m_playerInputController = PlayerInputController.instance;
        m_levelUIController = LevelUIController.instance;

        SubscribeToEvents();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SubscribeToEvents()
    {
        m_playerInputController.OnSwitchPlayer.AddListener(SwitchPlayer);
        
        m_playerInputController.OnToggleLevelMenu.AddListener(ToggleLevelMenu);
        m_playerInputController.OnRestartLevel.AddListener(Restart);

        m_levelUIController.OnPlayerSelectButtonPressed += SelectPlayer;

        m_levelUIController.OnResumeButtonPressed += ToggleLevelMenu;
        m_levelUIController.OnRestartButtonPressed += Restart;
        m_levelUIController.OnMainMenuButtonPressed += GoToMainMenu;

    }


    private void SelectPlayer(int playerUINumber)
    {
        Debug.Log("Received OnPlayerSelectButtonPressed, from LevelUIController, to LevelController");
        m_playerSelectionController.SelectPlayer(playerUINumber - 1);
    }

    private void ToggleLevelMenu()
    {
        Debug.Log("Receiveing OnToggleLevelMenu from PlayerInputController, or OnResumeButtonPressed from LevelUIController, to LevelController");
        bool menuIsActive = m_levelMainMenu.activeSelf;
        m_levelMainMenu.SetActive(!menuIsActive);
    }

    private void GoToMainMenu()
    {
        Debug.Log("Receiveing OnMainMenuButtonPressed, from LevelUIController, to LevelController");
        m_gameManager.MainMenu();
    }

    private void Restart()
    {
        Debug.Log("Receiveing OnRestartButtonPressed from LevelUIController, or OnRestartLevel from PlayerInputController, to LevelController");
        m_gameManager.RestartLevel();
    }


    private void SwitchPlayer()
    {
        Debug.Log("Receiveing OnSwitchPlayer, from PlayerInputController, to LevelController");
        m_playerSelectionController.SwitchSelectedPlayer();
    }


}

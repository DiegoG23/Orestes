using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerInputController : MonoBehaviour
{
    private GameManager m_gameManager;
    private PlayerSelectionController m_playerSelectionController;

    // PLAYER SELECTION
    [SerializeField] private KeyCode m_switchPlayerKeyCode = KeyCode.Tab;
    [SerializeField] private KeyCode m_selectPlayerOneKeyCode = KeyCode.Alpha1;
    [SerializeField] private KeyCode m_selectPlayerTwoKeyCode = KeyCode.Alpha2;
    //[SerializeField] private KeyCode m_selectPlayerThreeKeyCode = KeyCode.Alpha3;
    //[SerializeField] private KeyCode m_selectPlayerFourKeyCode = KeyCode.Alpha4;

    // PLAYER ACTIONS
    [SerializeField] protected KeyCode m_attackKeyCode = KeyCode.A;
    [SerializeField] protected KeyCode m_abilityOneKeyCode = KeyCode.S;
    [SerializeField] protected KeyCode m_abilityTwoKeyCode = KeyCode.D;
    [SerializeField] protected KeyCode m_abilityThreeKeyCode = KeyCode.F;
    [SerializeField] protected KeyCode m_crouchKeyCode = KeyCode.C;

    // GAME PERSISTANCE
    [SerializeField] private KeyCode m_saveLevelKeyCode = KeyCode.F5;
    [SerializeField] private KeyCode m_loadLevelKeyCode = KeyCode.F8;
    [SerializeField] private KeyCode m_restartLevelKeyCode = KeyCode.R;
    [SerializeField] private KeyCode m_levelMenuKeyCode = KeyCode.Escape;

    //ACTIONS
    public UnityEvent OnToggleLevelMenu;
    public UnityEvent OnRestartLevel;
    public UnityEvent OnSwitchPlayer;



    public static PlayerInputController instance;
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
        m_gameManager = GameManager.instance;
        m_playerSelectionController = PlayerSelectionController.instance;
    }

    private void Update()
    {
        ClickHandler();
        KeyboardHandler();
    }


    private void KeyboardHandler()
    {
        // TODO Analizar cambiar ifelses por un Switch


        // PLAYER SELECTION
        if (Input.GetKeyDown(m_switchPlayerKeyCode))
        {
            OnSwitchPlayer.Invoke();
        }
        else if (Input.GetKeyDown(m_selectPlayerOneKeyCode))
        {
            m_playerSelectionController.SelectPlayer(0);
        }
        else if (Input.GetKeyDown(m_selectPlayerTwoKeyCode))
        {
            m_playerSelectionController.SelectPlayer(1);
        }

        // PLAYER ACTIONS
        else if (Input.GetKeyDown(m_crouchKeyCode))
        {
            foreach (Player l_player in m_playerSelectionController.GetSelectedPlayers())
            {
                l_player.ToggleCrouch();
            }
        }

        else if (Input.GetKeyDown(m_abilityOneKeyCode))
        {
            m_playerSelectionController.GetSelectedPlayer()?.TriggerAbilityOne();
        }



        // GAME PERSISTANCE
        else if (Input.GetKeyDown(m_saveLevelKeyCode))
        {
            m_gameManager.SaveLevel();
        }
        else if (Input.GetKeyDown(m_loadLevelKeyCode))
        {
            m_gameManager.LoadLevel();
        }
        else if (Input.GetKeyDown(m_restartLevelKeyCode))
        {
            OnRestartLevel?.Invoke();
            //m_gameManager.RestartLevel();
        }


        else if (Input.GetKeyDown(m_levelMenuKeyCode))
        {
            OnToggleLevelMenu?.Invoke();
        }
    }


    public void ClickHandler()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Moving Player!!!");
            RaycastHit hit;
            LayerMask layerMask = LayerMask.GetMask("PlayerNav", "Pickup");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.layer == LayerMask.NameToLayer("PlayerNav"))
                {
                    // The raycast hit an object on the "PlayerNav" layer.
                    Debug.Log("PLAYER NAV HIT!!!!!");

                    foreach (Player l_player in m_playerSelectionController.GetSelectedPlayers())
                    {
                        l_player.MoveTo(hit.point);
                    }
                }
                else if (hitObject.layer == LayerMask.NameToLayer("Pickup"))
                {
                    // The raycast hit an object on the "Pickup" layer.
                    Debug.Log("PICKUP HIT!!!!!");
                    Pickup pickup = hitObject.GetComponent<Pickup>();

                    foreach (Player l_player in m_playerSelectionController.GetSelectedPlayers())
                    {
                        l_player.MoveTo(pickup);
                    }
                }
            }
        }
    }
}

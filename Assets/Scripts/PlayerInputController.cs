using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private GameManager m_gameManager;
    private PlayerSelectionController m_playerSelectionController;

    // PLAYER SELECTION
    [SerializeField] private KeyCode m_switchPlayerKeyCode = KeyCode.Tab;

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
            else
            {
                Destroy(gameObject);
            }
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
        // PLAYER SELECTION
        if (Input.GetKeyDown(m_switchPlayerKeyCode))
        {
            m_playerSelectionController.SwitchSelectedPlayer();
        }

        // PLAYER ACTIONS
        if (Input.GetKeyDown(m_crouchKeyCode))
        {
            foreach (Player l_player in m_playerSelectionController.GetSelectedPlayers())
            {
                l_player.ToggleCrouch();
            }
        }

        if (Input.GetKeyDown(m_abilityOneKeyCode))
        {
            m_playerSelectionController.GetSelectedPlayer()?.TriggerAbilityOne();
        }

        // GAME PERSISTANCE
        if (Input.GetKeyDown(m_saveLevelKeyCode))
        {
            m_gameManager.SaveLevel();
        }
        else if (Input.GetKeyDown(m_loadLevelKeyCode))
        {
            m_gameManager.LoadLevel();
        }
        else if (Input.GetKeyDown(m_restartLevelKeyCode))
        {
            m_gameManager.RestartLevel();
        }
    }


    public void ClickHandler()
    {
        if (Input.GetMouseButtonDown(0))
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

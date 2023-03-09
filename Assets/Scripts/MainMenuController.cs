using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Animator ElectraAnimator;
    [SerializeField] Animator IcarusAnimator;
    [SerializeField] Animator DroneAnimator;

    private GameManager m_gameManager;

    public static MainMenuController instance;
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

        ElectraAnimator.SetBool("isMainMenu", true);
        IcarusAnimator.SetBool("isMainMenu", true);
        DroneAnimator.SetBool("isMainMenu", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void NewGame()
    {
        m_gameManager.NewGame();
    }

    public void Credits()
    {
        m_gameManager.Credits();

    }

    public void Quit()
    {
        m_gameManager.QuitGame();

    }

}

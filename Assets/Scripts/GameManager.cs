using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;

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


}

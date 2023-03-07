using System;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject m_levelMainMenu;

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
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {

    }

    private void Update()
    {

    }


    public void NextLevel()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Level_1");
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

    public void ToggleMainMenu()
    {
        return;
        //TODO arreglar main menu
        /*
        bool menuIsActive = m_levelMainMenu.activeSelf;
        m_levelMainMenu.SetActive(!menuIsActive);
        */
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}

using System;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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


    public void NewGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Level_1",LoadSceneMode.Single);
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

    public void QuitGame()
    {
        Application.Quit();
    }
}

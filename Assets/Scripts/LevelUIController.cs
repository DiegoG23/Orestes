using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelUIController : MonoBehaviour
{
    // HUD EVENTS
    public event Action<int> OnPlayerSelectButtonPressed;
    
    /*
    public UnityEvent OnAbilityOneButtonPressed;
    public UnityEvent OnAbilityTwoButtonPressed;
    public UnityEvent OnAbilityThreeButtonPressed;
    */

    // LEVEL MENU EVENTS
    public event Action OnResumeButtonPressed;
    public event Action OnRestartButtonPressed;
    public event Action OnMainMenuButtonPressed;



    public static LevelUIController instance;
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

    public void SelectPlayer(int playerUINumber)
    {
        OnPlayerSelectButtonPressed.Invoke(playerUINumber);
    }

    public void ResumeButtonPressed()
    {
        OnResumeButtonPressed.Invoke();
    }

    public void RestartButtonPressed()
    {
        OnRestartButtonPressed.Invoke();
    }

    public void MainMenuButtonPressed()
    {
        OnMainMenuButtonPressed.Invoke();
    }

}

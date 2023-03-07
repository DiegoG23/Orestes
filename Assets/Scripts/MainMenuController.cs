using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Animator ElectraAnimator;
    [SerializeField] Animator IcarusAnimator;
    [SerializeField] Animator DroneAnimator;

    void Start()
    {
        ElectraAnimator.SetBool("isMainMenu", true);
        IcarusAnimator.SetBool("isMainMenu", true);
        DroneAnimator.SetBool("isMainMenu", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

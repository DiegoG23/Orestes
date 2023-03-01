using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider collider)
    {

        GameObject other = collider.gameObject;
        if (other != null
            && other.CompareTag("Player")
            && other.TryGetComponent<Player>(out var player))
        {
            GameManager.instance.WinLevel();
        }
    }
}

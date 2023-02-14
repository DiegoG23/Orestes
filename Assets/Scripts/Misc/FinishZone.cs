using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

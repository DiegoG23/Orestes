using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrinker : MonoBehaviour
{
    [SerializeField] float shrinkFactor = 0.2f;
    [SerializeField] float portalDelay = 0.2f;
    private float _currentTime;


    private void OnTriggerEnter(Collider collider)
    {
        GameObject other = collider.gameObject;
        if (other != null
            && other.CompareTag("Player")
            && _currentTime <= Time.time
            && other.TryGetComponent<HarryController>(out var harry))
        {
            _currentTime = Time.time + portalDelay;
            harry.TogglePortalShrink(shrinkFactor);
        }
    }
}

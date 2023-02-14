using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpWall : MonoBehaviour
{
    [SerializeField] private Transform[] transforms;
    [SerializeField] private float teleportContactTime;
    private float _teleportTime;


    public void TeleportWall()
    {
        Transform newTransform = transforms[Random.Range(0, transforms.Length)];
        transform.SetPositionAndRotation(newTransform.position, newTransform.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other != null 
            && other.CompareTag("Player") 
            && other.TryGetComponent<HarryController>(out var harry))
        {
            _teleportTime = Time.time + teleportContactTime;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other != null 
            && other.CompareTag("Player")
            && _teleportTime <= Time.time 
            && other.TryGetComponent<HarryController>(out var harry))
        {
            TeleportWall();
        }
    }

}

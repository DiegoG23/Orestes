using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    [SerializeField] private float lifetime = 5.0f;
    [SerializeField] private float explosionforce = 0.5f;
    [SerializeField] private float explosionradius = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        lifetime += Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Countdown();
    }

    private void Countdown()
    {
        if (lifetime <= Time.time)
        {
            KillPulse();
        }
    }

    private void KillPulse()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if(other.TryGetComponent(out Enemy enemy))
            {
                enemy.Disable();
                Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();
                //enemyRigidbody.AddExplosionForce(explosionforce, transform.position, explosionradius);
            }
        }
    }
}

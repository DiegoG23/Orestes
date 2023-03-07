using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zapper : MonoBehaviour
{
    [SerializeField] private float explosionforce = 0.25f;
    [SerializeField] private float explosionradius = 0.25f;
    [SerializeField] private Animator m_animator;

    private float lifetime; //get from the animation length

    void Start()
    {
        lifetime = m_animator.GetCurrentAnimatorStateInfo(0).length;
        Debug.Log($"pulse lifetime = {lifetime}");
        lifetime += Time.time;
    }


    void Update()
    {
        Countdown();
    }

    private void Countdown()
    {
        if (lifetime <= Time.time)
        {
            KillZapper();
        }
    }

    private void KillZapper()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if(other.TryGetComponent(out Enemy enemy))
            {
                Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();
                enemyRigidbody.AddExplosionForce(explosionforce, transform.position, explosionradius);
                enemy.Disable();
            }
        }
    }
}

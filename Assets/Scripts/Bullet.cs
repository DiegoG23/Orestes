using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    [SerializeField] private float lifetime = 2f;


    private void Start()
    {
        lifetime += Time.time;
    }


    private void Update()
    {
        Move();
        Countdown();
    }

    private void Move()
    {
        this.transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void Countdown()
    {
        if (lifetime <= Time.time)
        {
            KillBullet();
        }
    }

    private void KillBullet()
    {
        Destroy(gameObject);
    }



}
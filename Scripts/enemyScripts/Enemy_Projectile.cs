
using UnityEngine;

public class Enemy_Projectile : Enemy_damage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;

    public void ActivateProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float MoveSpeed = speed * Time.deltaTime;
        transform.Translate(MoveSpeed, 0, 0);

        lifetime = Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); //allows the parent script to function
        gameObject.SetActive(false); //disables the projectile


    }
}

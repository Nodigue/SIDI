using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IEnemy
{
    public int maxHealth;
    private int currentHealth;

    public float knockbackForce;
    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public void PerformAttack(int damage)
    {
        // Ne fait rien pour un target dummy
    }

    public void TakeDamage(int amount, Transform obj)
    {
        // Knockback
        Vector2 knockbackDir = (transform.position - obj.transform.position).normalized;
        rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);


        // Réduction de vie
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}

using UnityEngine;

public interface IEnemy
{
    void PerformAttack(int damage);
    void TakeDamage(int amount, Transform obj);
    void Die();
}

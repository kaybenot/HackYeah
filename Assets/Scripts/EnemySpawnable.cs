using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using Lemurs.Enemies;

public class EnemySpawnable : MonoBehaviour
{
    [SerializeField] private EnemyBattler battler;
    
    public Action OnDeath;

    [ContextMenu("Invoke Die")]
    public void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    public void Slowdown()
    {
        battler.Slowdown();
    }

    public void DealDamage(int damage)
    {
        battler.Damage(damage);
        
        if (battler.Health <= 0)
        {
            Die();
        }
    }
}

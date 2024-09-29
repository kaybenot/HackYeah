using UnityEngine;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Lemurs.Enemies;
using Random = UnityEngine.Random;

public class EnemySpawnable : MonoBehaviour
{
    [SerializeField] private EnemyBattler battler;
    [SerializeField] private List<AudioClip> deathSFX;
    
    public Action OnDeath;

    [ContextMenu("Invoke Die")]
    public void Die()
    {
        if (deathSFX.Count > 0)
            AudioSource.PlayClipAtPoint(deathSFX[Random.Range(0, deathSFX.Count)], transform.position);
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

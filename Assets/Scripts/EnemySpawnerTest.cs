using UnityEngine;
using System;

public class EnemySpawnerTest : MonoBehaviour
{
    public event EventHandler OnDeath;
    public float speed = 5f;
    public float health = 100f;
    void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.up);
    }
    
    public void Die()
    {
        OnDeath?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
    
    [ContextMenu("Invoke Die")]
    void InvokeDie()
    {
        Die();
    }

    public void Slowdown()
    {
        var amount = 1.0f;
        transform.position -= new Vector3(0, amount, 0);
    }
    
    public void DealDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }
}

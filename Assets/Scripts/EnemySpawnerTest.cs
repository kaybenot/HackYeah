using UnityEngine;
using System;

public class EnemySpawnerTest : MonoBehaviour
{
    public event EventHandler OnDeath;
    public float speed = 5f;
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
}

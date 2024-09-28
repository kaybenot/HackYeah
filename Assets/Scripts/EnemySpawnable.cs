using UnityEngine;
using System;

public class EnemySpawnable : MonoBehaviour
{
    public event EventHandler OnDeath;

    [ContextMenu("Invoke Die")]
    public void Die()
    {
        OnDeath?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}

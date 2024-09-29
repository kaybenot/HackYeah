using UnityEngine;
using System;

public class EnemySpawnable : MonoBehaviour
{
    public event EventHandler OnDeath;
    public float speed;
    private void Update()
    {
        transform.position += Time.deltaTime * Vector3.up;
    }

    [ContextMenu("Invoke Die")]
    public void Die()
    {
        OnDeath?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}

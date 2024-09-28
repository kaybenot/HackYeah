using System;
using UnityEngine;

public class ProjectileSingle : MonoBehaviour
{
    public float damage = 10f;
    public float selfDestructTime = 5f;

    private void Start()
    {
        Destroy(gameObject, selfDestructTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
            return;
        
        var enemyScript = other.gameObject.GetComponent<EnemySpawnerTest>();
        if(enemyScript != null)
        {
            enemyScript.DealDamage(damage);
        }
        else
        {
            Debug.LogError("No Enemy script on Enemy Tag object!");
        }
        Destroy(gameObject);
    }
}

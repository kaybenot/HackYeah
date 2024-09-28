using System;
using UnityEngine;

public class ProjectileUtility : MonoBehaviour
{
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
            enemyScript.Slowdown();
        }
        else
        {
            Debug.LogError("No Enemy script on Enemy Tag object!");
        }
        Destroy(gameObject);
    }
}

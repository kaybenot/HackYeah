using System;
using UnityEngine;

public class ProjectileArea : MonoBehaviour
{
    public float damage = 10f;
    public float selfDestructTime = 5f;
    public float radius = 5f;

    private void Start()
    {
        Destroy(gameObject, selfDestructTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
            return;
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                var enemyScript = hitCollider.GetComponent<EnemySpawnerTest>();
                if (enemyScript != null)
                {
                    enemyScript.DealDamage(damage);
                }
                else
                {
                    Debug.LogError("No Enemy script on Enemy Tag object!");
                }
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileSingle : MonoBehaviour
{
    public List<AudioClip> hitSFX;
    public int damage = 10;
    public float selfDestructTime = 5f;

    private void Start()
    {
        Destroy(gameObject, selfDestructTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
            return;
        
        var enemyScript = other.gameObject.GetComponent<EnemySpawnable>();
        if(enemyScript != null)
        {
            enemyScript.DealDamage(damage);
            
            if (hitSFX.Count > 0)
            {
                AudioSource.PlayClipAtPoint(hitSFX[Random.Range(0, hitSFX.Count)], transform.position);
            }
        }
        else
        {
            Debug.LogError("No Enemy script on Enemy Tag object!");
        }
        Destroy(gameObject);
    }
}

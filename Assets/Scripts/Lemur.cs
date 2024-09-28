using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class Lemur : MonoBehaviour
{
    public float fireRate;
    public GameObject projectile;
    public float projectileSpeed;
    //public Transform firePoint;
    private float _lastFireTime;
    public BoxCollider[] targettingColliders;
    
    void Update()
    {
        if(_lastFireTime + fireRate < Time.time)
        {
            TryFire();
        }
    }
    
    private void TryFire()
    {
        List<Vector3> foundTargets = new List<Vector3>();
        
        foreach (BoxCollider collider in targettingColliders)
        {
            Collider[] hitColliders = Physics.OverlapBox(collider.bounds.center, collider.bounds.extents, collider.transform.rotation);
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider != collider && hitCollider.CompareTag("Enemy"))
                {
                    // Fire projectile
                    _lastFireTime = Time.time;
                    foundTargets.Add(hitCollider.GetComponent<Transform>().position);
                }
            }
        }
        
        if (foundTargets.Any())
        {
            Vector3 highestTarget = foundTargets.OrderByDescending(target => target.y).First();
            Fire(highestTarget);
        }
    }

    private void Fire(Vector3 target)
    {
        // Calculate direction to the target
        Vector3 direction = (target - transform.position).normalized;

        // Instantiate the projectile
        GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.LookRotation(direction));

        // Set the velocity of the projectile
        newProjectile.GetComponent<Rigidbody>().linearVelocity = direction * projectileSpeed;
    }
}

using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class Lemur : MonoBehaviour
{
    public AudioClip fireSFX;
    public float fireRate;
    public GameObject projectile;
    public float projectileSpeed;
    public Transform firePoint;
    public BoxCollider[] targettingColliders;
    private float _lastFireTime;
    private Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
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
        Vector3 direction = (target - firePoint.position).normalized;

        // Instantiate the projectile
        GameObject newProjectile = Instantiate(projectile, firePoint.position, Quaternion.LookRotation(direction));

        // Set the velocity of the projectile
        newProjectile.GetComponent<Rigidbody>().linearVelocity = direction * projectileSpeed;
        
        if (animator != null)
        {
            animator.SetTrigger("Fire");
        }
        
        if (fireSFX != null)
            AudioSource.PlayClipAtPoint(fireSFX, transform.position);
    }
}

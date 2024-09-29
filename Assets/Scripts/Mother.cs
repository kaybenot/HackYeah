using System;
using UnityEngine;

public class Mother : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Range(0f, 1f)] private float TopScreenOffset = 0.1f;
    [SerializeField] private LayerMask treeLayerMask;
    [SerializeField] private float distanceFromTree;
    [SerializeField, Range(0f, 1f)] private float movementSmoothingValue = 0.5f;

    public Action OnMotherHit;
    
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("Mother script did not find Main Camera!");
        }
    }

    private void Start()
    {
        var target = GetMotherTarget();
        if (target != null)
            Teleport(target.Value.point, target.Value.normal);
    }

    private void MoveTo(Vector3 pos, Vector3 normal)
    {
        // TODO: Move anim
        Teleport(pos, normal, movementSmoothingValue);
    }

    private void Teleport(Vector3 pos, Vector3 normal, float smoothing = 1)
    {
        transform.position = Vector3.Lerp(transform.position, pos + normal * distanceFromTree, smoothing); 
    }

    private (Vector3 point, Vector3 normal)? GetMotherTarget()
    {
        var ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height * (1 - TopScreenOffset)));
        if (Physics.Raycast(ray, out var hit, float.PositiveInfinity, treeLayerMask))
        {
            return (hit.point, hit.normal);
        }

        return null;
    }

    private void FixedUpdate()
    {
        var target = GetMotherTarget();
        if (target != null)
            MoveTo(target.Value.point, target.Value.normal);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            OnMotherHit?.Invoke();
        }
    }
}

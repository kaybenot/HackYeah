using System;
using Unity.Cinemachine;
using UnityEngine;

public class Mother : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Range(0f, 1f)] private float TopScreenOffset = 0.1f;
    [SerializeField] private LayerMask treeLayerMask;
    [SerializeField] private float distanceFromTree;
    [SerializeField, Range(0f, 1f)] private float movementSmoothingValue = 0.5f;
    [SerializeField] private float rotationSpeed;

    public Action OnMotherHit;
    
    private Camera cam;

    private Vector3 previousPosition;
    private Quaternion targetRotation;

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
        previousPosition = transform.position;
        targetRotation = transform.rotation;

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
        if (target == null)
            return;

        MoveTo(target.Value.point, target.Value.normal);

        var forward = transform.position - previousPosition;
        if (forward.sqrMagnitude > 0.0001f)
        {
            targetRotation = Quaternion.LookRotation(forward, target.Value.normal);
            previousPosition = transform.position;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlayerPrefs.SetString("death_cause", "They got you");
            OnMotherHit?.Invoke();
            GameState.Instance.GameLost();
        }
    }
}

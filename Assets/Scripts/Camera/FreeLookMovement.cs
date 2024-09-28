using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class FreeLookMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float cameraSpeedX = 100;
    [SerializeField] private float cameraSpeedY = 1;
    [SerializeField] private float maxY = 1;
    [SerializeField] private float minY = -1;

    [Header("References")]
    [SerializeField] private CinemachineOrbitalFollow orbitalFollow;
    [SerializeField] private CinemachineHardLookAt hardLookAt;
    
    private float x;
    private float y;
    
    public void MoveX(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            x = 0f;
            y = 0f;
        }
        
        if (!context.performed)
            return;

        var val = context.ReadValue<Vector2>();
        x = val.x;
        y = val.y;
    }

    private void OnValidate()
    {
        if (maxY < minY)
            maxY = minY;
    }

    private void Update()
    {
        orbitalFollow.HorizontalAxis.Value -= x * Time.unscaledDeltaTime * cameraSpeedX;
        SetOffsetY(y * Time.unscaledDeltaTime * cameraSpeedY + orbitalFollow.TargetOffset.y);
    }

    private void SetOffsetY(float offsetY)
    {
        orbitalFollow.TargetOffset.y = offsetY;
        hardLookAt.LookAtOffset.y = offsetY;
        if (orbitalFollow.TargetOffset.y > maxY)
        {
            orbitalFollow.TargetOffset.y = maxY;
            hardLookAt.LookAtOffset.y = maxY;
        }
        else if (orbitalFollow.TargetOffset.y < minY)
        {
            orbitalFollow.TargetOffset.y = minY;
            hardLookAt.LookAtOffset.y = minY;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + Vector3.up * maxY - orbitalFollow.TargetOffset, Vector3.back);
        Gizmos.DrawRay(transform.position + Vector3.up * minY - orbitalFollow.TargetOffset, Vector3.back);
    }
}

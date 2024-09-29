using System;
using UnityEngine;

public class MotherTowerSpawner : MonoBehaviour
{
    [SerializeField] private LayerMask treeMask;
    
    private void Start()
    {
        if (SequenceController.Instance != null)
        {
            SequenceController.Instance.OnTryTowerSpawn += SpawnTower;
        }
        else
        {
            Debug.LogError("Could not find SequenceController instance!");
            enabled = false;
        }
    }

    private void SpawnTower(Tower tower)
    {
        var position = GetSpawnPosition();
        if (position == null)
            return;
        
        var normal = GetTowerNormalAtPosition(transform.position);
        if (normal == null)
            return;

        Instantiate(tower.TowerPrefab, position.Value, Quaternion.LookRotation(normal.Value));
    }

    private Vector3? GetSpawnPosition()
    {
        if (Camera.main == null)
            return null;
        
        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out var hit, float.PositiveInfinity, treeMask))
        {
            return hit.point;
        }
        
        return transform.position;
    }

    private Vector3? GetTowerNormalAtPosition(Vector3 position)
    {
        if (Camera.main == null)
            return null;

        if (Physics.Raycast(position, Camera.main.transform.forward, out var hit, float.PositiveInfinity, treeMask))
        {
            return hit.normal;
        }

        return null;
    }
}

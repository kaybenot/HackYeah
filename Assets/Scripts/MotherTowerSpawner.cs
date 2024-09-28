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
        var normal = GetTowerNormalAtPosition(position);
        if (normal == null)
            return;

        Instantiate(tower.TowerPrefab, position, Quaternion.LookRotation(normal.Value));
    }

    private Vector3 GetSpawnPosition()
    {
        // TODO: More accurate way of spawning here
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

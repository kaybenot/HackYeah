using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Branch
{
    public Branch(int @towerSlots)
    {
        this.towerSlots = @towerSlots;
        towers = new List<Tower>();
    }

    public Action<Tower> OnTowerAdded;
    public Action<Tower> OnTowerRemoved;

    private int towerSlots;
    private List<Tower> towers;
    
    /// <returns>True if there was a free slot</returns>
    public bool AddTower(Tower tower)
    {
        if (towers.Count >= towerSlots)
            return false;
        
        towers.Add(tower);
        OnTowerAdded?.Invoke(tower);
        return true;
    }
    
    /// <param name="id">Card ID</param>
    public void RemoveTower(int id)
    {
        for (var i = 0; i < towers.Count; i++)
        {
            if (id != towers[i].ID)
                continue;

            var tower = towers[i];
            towers.RemoveAt(i);
            OnTowerRemoved?.Invoke(tower);
            return;
        }
    }

    public IEnumerable<Tower> GetTowers()
    {
        return towers;
    }
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerList", menuName = "Engine/TowerList")]
public class TowerList : ScriptableObject
{
    public List<Tower> Towers;
}

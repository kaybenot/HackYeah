using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class SequenceController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TowerList towerList;

    [CanBeNull] public static SequenceController Instance;
    public Action<Tower> OnTryTowerSpawn;
    
    private List<SequenceButton> ButtonList = new();
    private List<float> towerCooldowns = new();

    public IEnumerable<float> GetTowerCooldowns => towerCooldowns;

    private void Awake()
    {
        Instance = this;

        foreach (var tower in towerList.Towers)
        {
            towerCooldowns.Add(0f);
        }
    }

    public void OnSequenceButton0(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        
        ProcessButton(SequenceButton.Button0);
    }
    
    public void OnSequenceButton1(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        
        ProcessButton(SequenceButton.Button1);
    }
    
    public void OnSequenceButton2(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        
        ProcessButton(SequenceButton.Button2);
    }
    
    public void OnSequenceButton3(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        
        ProcessButton(SequenceButton.Button3);
    }

    private void ProcessButton(SequenceButton button)
    {
        ButtonList.Add(button);
        CheckForSequence();
    }

    private void CheckForSequence()
    {
        for (var j = 0; j < ButtonList.Count; j++)
        {
            var towers = new List<Tower>(towerList.Towers);

            for (var i = 0; i < ButtonList.Count; i++)
            {
                towers = towers.Where(t => i < t.Sequence.ButtonList.Count && i + j < ButtonList.Count && t.Sequence.ButtonList[i] == ButtonList[i + j]).ToList();
                if (towers.Count == 1 && towers[0].Sequence.ButtonList.Count == i + 1)
                {
                    Debug.Log($"Trying to spawn tower: {towers[0].TowerPrefab.name}");
                    var index = -1;
                    for (var k = 0; k < towerList.Towers.Count; k++)
                    {
                        if (towerList.Towers[k].TowerPrefab == towers[0].TowerPrefab)
                        {
                            index = k;
                        }
                    }

                    if (index >= 0 && towerCooldowns[index] <= 0f)
                    {
                        towerCooldowns[index] = towers[0].SpawnCooldown;
                        OnTryTowerSpawn?.Invoke(towers[0]);
                    }
                    ButtonList.Clear();
                    return;
                }
            }
        }
    }

    private void Update()
    {
        for (var i = 0; i < towerCooldowns.Count; i++)
        {
            if (towerCooldowns[i] > 0f)
            {
                towerCooldowns[i] -= Time.deltaTime;
            }
        }
    }
}

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

    private void Awake()
    {
        Instance = this;
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
                    OnTryTowerSpawn?.Invoke(towers[0]);
                    Debug.Log($"Trying to spawn tower: {towers[0].TowerPrefab.name}");
                    ButtonList.Clear();
                    return;
                }
            }
        }
    }
}

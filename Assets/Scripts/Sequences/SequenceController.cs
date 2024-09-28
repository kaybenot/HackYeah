using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class SequenceController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SequenceSettings sequenceSettings;

    [CanBeNull] public static SequenceController Instance;
    public Action<int> OnSequenceInvoked;
    
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
            var seqs = new List<Sequence>(sequenceSettings.Sequences);
            
            for (var i = 0; i < ButtonList.Count; i++)
            {
                seqs = seqs.Where(s => i < s.ButtonList.Count && i + j < ButtonList.Count && s.ButtonList[i] == ButtonList[i + j]).ToList();
                if (seqs.Count == 1 && seqs[0].ButtonList.Count == i + 1)
                {
                    OnSequenceInvoked?.Invoke(seqs[0].ID);
                    Debug.Log($"Invoked sequence: {seqs[0].ID}");
                    ButtonList.Clear();
                    return;
                }
            }
        }
    }
}

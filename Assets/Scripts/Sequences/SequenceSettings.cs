using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SequenceSettings", menuName = "Engine/SequenceSettings")]
public class SequenceSettings : ScriptableObject
{
    public List<Sequence> Sequences;
}

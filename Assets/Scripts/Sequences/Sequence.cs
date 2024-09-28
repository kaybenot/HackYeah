using System;
using System.Collections.Generic;
using UnityEngine;

public enum SequenceButton
{
    Button0,
    Button1,
    Button2,
    Button3
}

[Serializable]
public class Sequence
{
    public List<SequenceButton> ButtonList;
    public int ID;
}

using System;
using UnityEngine;

[Serializable]
public class Tower
{
    public int ID = -1;
    public float RateOfFire;
    public float RelativeRangeMin;
    public float RelativeRangeMax;

    public void Validate()
    {
        if (ID == -1)
        {
            Debug.LogWarning("Invalid tower ID!");
        }
        
        if (RelativeRangeMin > RelativeRangeMax)
        {
            Debug.LogWarning("Range min range was higher than max!");
            RelativeRangeMin = RelativeRangeMax;
        }
    }
}

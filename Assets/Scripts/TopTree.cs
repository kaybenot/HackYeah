using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TopTree : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlayerPrefs.SetString("death_cause", "Enemies reached the top");
            GameState.Instance.GameLost();
        }
    }
}

using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TopTree : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameState.Instance.GameLost();
        }
    }
}

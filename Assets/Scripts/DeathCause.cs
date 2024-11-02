using System;
using TMPro;
using UnityEngine;

public class DeathCause : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private void Start()
    {
        text.text = PlayerPrefs.GetString("death_cause", "");
    }
}

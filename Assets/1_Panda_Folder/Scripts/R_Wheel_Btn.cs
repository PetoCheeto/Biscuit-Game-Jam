using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class R_Wheel_Btn : MonoBehaviour
{
    private R_Wheel r_Wheel;
    private Button thisButton;

    private void Awake()
    {
        thisButton = GetComponent<Button>();
        r_Wheel = FindFirstObjectByType<R_Wheel>();
    }

    private void Start()
    {
        WarningOfMissingComponents();
    }

    public void CheckButtonStatus()
    {
        if (r_Wheel.IsSpinning == true) thisButton.interactable = false;
        else thisButton.interactable = true;
    }

    private void WarningOfMissingComponents()
    {
        if (r_Wheel == null) Debug.Log("You are missing the roulette wheel class in this scene! " +
            "This will cause errors.");
    }
}

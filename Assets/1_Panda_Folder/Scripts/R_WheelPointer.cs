using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_WheelPointer : MonoBehaviour
{
    private R_Wheel rouletteWheel;

    private void Awake()
    {
        rouletteWheel = FindFirstObjectByType<R_Wheel>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<R_WheelSquare>() != null)
            rouletteWheel.SquareLandedOn(other.GetComponent<R_WheelSquare>());
    }
}

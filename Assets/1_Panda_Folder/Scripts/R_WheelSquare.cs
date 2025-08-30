using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_WheelSquare : MonoBehaviour
{
    [Header("Square Settings")]
    public WheelBettingColor squareColor;
    public SquareMultiAmount squareMultiAmount;
}

public enum SquareMultiAmount
{
    None,
    X2,
    X4
}

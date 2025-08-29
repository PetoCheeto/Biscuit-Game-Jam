using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Wheel : MonoBehaviour
{
    [Header("Wheel Settings")]
    [SerializeField] private SpinDirection spinDirection = SpinDirection.Right;
    [SerializeField] private float minRotationSpeed = 800f;
    [SerializeField] private float maxRotationSpeed = 1800f;
    private float rotationSpeed;
    [SerializeField] private float spinDuration = 3f;

    public bool IsSpinning { get; private set; }

    // References
    private R_Wheel_Btn wheelButton;

    private void Awake()
    {
        wheelButton = FindFirstObjectByType<R_Wheel_Btn>();
    }

    private void Start()
    {
        WarningOfMissingComponents();
    }

    public void SpinTheWheel()
    {
        AssignRotationValue();
        StartCoroutine(SpinWheelCo());
    }

    private IEnumerator SpinWheelCo()
    {
        IsSpinning = true;
        float elaspedTime = 0f;
        float currentValue;
        wheelButton.CheckButtonStatus();
        while(elaspedTime < spinDuration)
        {
            elaspedTime += Time.deltaTime;
            float t = elaspedTime / spinDuration;
            currentValue = Mathf.Lerp(rotationSpeed, 0f, t);
            
            transform.Rotate(0f, 0f, currentValue * Time.deltaTime);
            yield return null;
        }

        IsSpinning = false;
        wheelButton.CheckButtonStatus();
    }

    private void AssignRotationValue()
    {
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);

        if (spinDirection == SpinDirection.Right) rotationSpeed *= -1f;
    }

    private void WarningOfMissingComponents()
    {
        if (wheelButton == null) Debug.Log("You are missing the wheel button class in this scene! " +
            "This will cause errors.");
    }
}

public enum SpinDirection
{ 
    Right,
    Left
}

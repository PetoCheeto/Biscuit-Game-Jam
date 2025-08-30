using System.Collections;
using TMPro;
using UnityEngine;

public class R_Wheel : MonoBehaviour
{
    [Header("Betting")]
    [SerializeField] private int costOfBet;
    [SerializeField] private WheelBettingColor bettingColor;

    [Header("Wheel Settings")]
    [SerializeField] private SpinDirection spinDirection = SpinDirection.Right;
    [SerializeField] private float minRotationSpeed = 800f;
    [SerializeField] private float maxRotationSpeed = 1800f;
    private float rotationSpeed;
    [SerializeField] private float spinDuration = 3f;

    public bool IsSpinning { get; private set; }

    [Header("Other")]
    public int money;
    public R_WheelSquare currentSquare;
    public TextMeshProUGUI bettingTMP;
    public TextMeshProUGUI resultsTMP;

    // References
    private R_Wheel_Btn wheelButton;

    private void Awake()
    {
        wheelButton = FindFirstObjectByType<R_Wheel_Btn>();
    }

    private void Start()
    {
        WarningOfMissingComponents();
        DisplayBettingText();
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
        TurnOffResultText();
        while (elaspedTime < spinDuration)
        {
            elaspedTime += Time.deltaTime;
            float t = elaspedTime / spinDuration;
            currentValue = Mathf.Lerp(rotationSpeed, 0f, t);

            transform.Rotate(0f, 0f, currentValue * Time.deltaTime);
            yield return null;
        }

        IsSpinning = false;
        wheelButton.CheckButtonStatus();
        DisplayResultText();
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

    public void ChangeBetteringColor(int _colorToBet)
    {
        if(_colorToBet == 0) bettingColor = WheelBettingColor.Red;
        else if (_colorToBet == 1) bettingColor = WheelBettingColor.Blue;
        else if (_colorToBet == 2) bettingColor = WheelBettingColor.Green;

        DisplayBettingText();
    }

    public void SquareLandedOn(R_WheelSquare _newSquare)
    {
        currentSquare = _newSquare;
    }

    private void DisplayBettingText()
    {
        switch(bettingColor)
        {
            case WheelBettingColor.Red:
                bettingTMP.text = "Betting On: Red";
                break;
            case WheelBettingColor.Blue:
                bettingTMP.text = "Betting On: Blue";
                break;
            case WheelBettingColor.Green:
                bettingTMP.text = "Betting On: Green";
                break;
        }
    }

    private void DisplayResultText()
    {
        if(!resultsTMP.gameObject.activeInHierarchy)
        {
            resultsTMP.gameObject.SetActive(true);
        }

        if (currentSquare.squareColor == bettingColor) resultsTMP.text = "YOU WON!";
        else resultsTMP.text = "YOU LOST!";
    }

    private void TurnOffResultText()
    {
        if (resultsTMP.gameObject.activeInHierarchy)
        {
            resultsTMP.gameObject.SetActive(false);
        }
    }
}

public enum SpinDirection
{
    Right,
    Left
}

public enum WheelBettingColor
{
    Blue,
    Red,
    Green
}

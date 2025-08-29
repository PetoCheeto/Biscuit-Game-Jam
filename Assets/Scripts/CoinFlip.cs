using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinFlip : MonoBehaviour
{
    public Button actionButton;
    public TextMeshProUGUI buttonText;
    public Outline one;
    public Outline two;
    public Outline three;
    public Outline four;
    public Outline five;
    public Outline six;
    [Header("coinflip")]
    public bool coinFlipSelected;
    public GameObject coin;
    public Sprite headsSprite;
    public Sprite tailsSprite;
    public float flipDuration = 1f;
    public int spinCount = 3;

    public SpriteRenderer sr;
    private bool isFlipping;
    public AudioClip coinAudio;
    [Header("diceroll")]
    public GameObject diceGuesses;
    public bool diceRollSelected;
    public GameObject dice;
    public int numberToRoll;
    public Sprite[] diceSprites;
    public SpriteRenderer diceSR;
    public int guessedNumber;
    public GameObject particle;
    public AudioSource audioSource;
    public AudioClip diceAudio;
    // Start is called before the first frame update
    void Start()
    {
        sr.sprite = headsSprite;
    }
    private void Update()
    {
        if(coinFlipSelected)
        {
            coin.SetActive(true);
            dice.SetActive(false);
            diceGuesses.SetActive(false);

            buttonText.text = "Flip";
        }
        if(diceRollSelected) 
        {
            coin.SetActive(false);
            dice.SetActive(true);
            diceGuesses.SetActive(true);

            buttonText.text = "Roll";

        }
    }
    public void SelectCoin() 
    {
        coinFlipSelected = true; 
        diceRollSelected = false;
        diceGuesses.SetActive(false);

        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(FlipCoin);
    }
    public void SelectDice() 
    {
        coinFlipSelected = false;
        diceRollSelected = true;
        diceGuesses.SetActive(true);

        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(RollDice);
        Debug.Log("select dice");
    }
    // Update is called once per frame
    public void FlipCoin()
    {
        if (!isFlipping && ItManager1.instance.its > 0)
        {
            ItManager1.instance.its--;
            audioSource.pitch = Random.Range(1f, 3f);
            audioSource.PlayOneShot(coinAudio);
            StartCoroutine(FlipCoroutine());
        }
    }
    public void Guess1() 
    {
        guessedNumber = 1;
        one.enabled = true;
        two.enabled = false;
        three.enabled = false;
        four.enabled = false;
        five.enabled = false;
        six.enabled = false;
    }
    public void Guess2() 
    {
        guessedNumber = 2;
        one.enabled = false;
        two.enabled = true;
        three.enabled = false;
        four.enabled = false;
        five.enabled = false;
        six.enabled = false;
    }
    public void Guess3() 
    {
        guessedNumber = 3;
        one.enabled = false;
        two.enabled = false;
        three.enabled = true;
        four.enabled = false;
        five.enabled = false;
        six.enabled = false;
    }
    public void Guess4() 
    {
        guessedNumber = 4;
        one.enabled = false;
        two.enabled = false;
        three.enabled = false;
        four.enabled = true;
        five.enabled = false;
        six.enabled = false;
    }
    public void Guess5()
    {
        guessedNumber = 5;
        one.enabled = false;
        two.enabled = false;
        three.enabled = false;
        four.enabled = false;
        five.enabled = true;
        six.enabled = false;
    }
    public void Guess6()
    {
        guessedNumber = 6;
        one.enabled = false;
        two.enabled = false;
        three.enabled = false;
        four.enabled = false;
        five.enabled = false;
        six.enabled = true;
    }
    public void RollDice() 
    {
        Debug.Log("roll");
        if(ItManager1.instance.its > 0) 
        {
            Instantiate(particle, dice.transform.position, dice.transform.rotation);
            audioSource.pitch = Random.Range(0.5f, 2f);
            audioSource.PlayOneShot(diceAudio);
            ItManager1.instance.its--;
            numberToRoll = Random.Range(1, 7);
            diceSR.sprite = diceSprites[numberToRoll - 1];
            if(numberToRoll == guessedNumber) 
            {
                ItManager1.instance.its += 10;
            }
        }
    }
    private System.Collections.IEnumerator FlipCoroutine()
    {
        isFlipping = true;

        // randomly decide result
        bool landOnHeads = Random.value > 0.5f;
        Sprite finalSprite = landOnHeads ? headsSprite : tailsSprite;

        float elapsed = 0f;
        float totalRot = 360f * spinCount + (landOnHeads ? 0f : 180f);
        // if tails, add half a spin so it ends flipped

        while (elapsed < flipDuration)
        {
            float rotation = Mathf.Lerp(0f, totalRot, elapsed / flipDuration);
            coin.transform.rotation = Quaternion.Euler(0f, rotation, 0f);

            // halfway logic for illusion
            if ((rotation % 360f) > 90f && (rotation % 360f) < 270f)
                sr.sprite = tailsSprite;
            else
                sr.sprite = headsSprite;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // lock in final result
        coin.transform.rotation = Quaternion.identity;
        sr.sprite = finalSprite;
        if(landOnHeads) 
        {
            ItManager1.instance.its += 2;
        }
        isFlipping = false;
    }
}
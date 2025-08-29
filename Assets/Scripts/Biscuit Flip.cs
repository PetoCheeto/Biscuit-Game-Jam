using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BiscuitFlip : MonoBehaviour
{

    [Header("coinflip")]
    public bool coinFlipSelected;
    public GameObject coin;
    public Sprite headsSprite;
    public Sprite tailsSprite;
    public float flipDuration = 1f;
    public int spinCount = 3;

    public Image sr;
    private bool isFlipping;
    public AudioSource audioSource;
    public AudioClip clip;

    public Image image;
    public GameObject winBiscuit;
    public GameObject loseBiscuit;
    public bool fadeImageIn;
    //    public AudioClip coinAudio;
    // Start is called before the first frame update
    void Start()
    {
        sr.sprite = headsSprite;
    }
    

    // Update is called once per frame
    public void FlipCoin()
    {
        if (!isFlipping && ItManager1.instance.its > 499)
        {
            ItManager1.instance.its--;

            StartCoroutine(FlipCoroutine());
        }
    }

    private void Update()
    {
        if(fadeImageIn)
        {
            image.gameObject.SetActive(true);
            var tempColor = image.color;
            tempColor.a += 0.05f;
            image.color = tempColor;
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
        if (landOnHeads)
        {
            ItManager1.instance.its += 2;
            winBiscuit.SetActive(true);
            fadeImageIn = true;
        }
        if(!landOnHeads) 
        {
            audioSource.pitch = 1;
            audioSource.PlayOneShot(clip);
            loseBiscuit.SetActive(true);
            fadeImageIn = true;

        }
        isFlipping = false;
    }
}
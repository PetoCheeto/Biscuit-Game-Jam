using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musicblend : MonoBehaviour
{
    public AudioSource songA;
    public AudioSource songB;
    public float maxValue = 500f;
    // Start is called before the first frame update
    void Start()
    {
        songA.Play();
        songB.Play();
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.Clamp01(ItManager1.instance.its / maxValue);
        float smoothT = Mathf.SmoothStep(0f, 1f, t);

        songA.volume = 1f - smoothT;
        songB.volume = smoothT;
    }
}

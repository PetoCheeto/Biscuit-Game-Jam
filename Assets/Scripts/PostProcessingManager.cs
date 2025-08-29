using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    [SerializeField] private Volume volume;

    private ChromaticAberration chroma;
    private Vignette vignette;
    public Bloom bloom;
    private FilmGrain filmGrain;
    public float targetChroma;
    public float targetVignette;
    public float targetBloom;
    public float targetFilmGrain;
    public static PostProcessingManager instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        // Get the overrides from the VolumeProfile
        if (volume != null && volume.profile != null)
        {
            volume.profile.TryGet(out chroma);
            volume.profile.TryGet(out vignette);
            volume.profile.TryGet(out bloom);
            volume.profile.TryGet(out filmGrain);
        }

    }
    private void Update()
    {
        float t = Mathf.InverseLerp(0f, 500f, ItManager1.instance.its);

        float newChroma = Mathf.Lerp(0f, targetChroma, t);
        float newVignette = Mathf.Lerp(0f, targetVignette, t);
        float newBloom = Mathf.Lerp(0f, targetBloom, t);
        float newFilmGrain = Mathf.Lerp(0f, targetFilmGrain, t);

        SetChromaticAberration(newChroma);
        SetVignette(newVignette);
        SetBloom(newBloom);
        SetFilmGrain(newFilmGrain);
    }

    public void SetChromaticAberration(float intensity)
    {
        if (chroma != null)
        {
            chroma.active = true;
            chroma.intensity.value = intensity;
        }
    }

    public void SetVignette(float intensity)
    {
        if (vignette != null)
        {
            vignette.active = true;
            vignette.intensity.value = intensity;
        }
    }

    public void SetBloom(float intensity)
    {
        if (bloom != null)
        {
            bloom.active = true;
            bloom.intensity.value = intensity;
        }
    }

    public void SetFilmGrain(float intensity)
    {
        if (filmGrain != null)
        {
            filmGrain.active = true;
            filmGrain.intensity.value = intensity;
        }
    }
}

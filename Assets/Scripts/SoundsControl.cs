using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundsControl : MonoBehaviour
{
    [SerializeField] private Slider[] sliders;
    [SerializeField] private AudioSource music;
    public static float volumeMusic;
    public static float volumeSounds;
    [SerializeField] private AudioSource[] sources;

    private void Start()
    {
        if (PlayerPrefs.HasKey("volumeM"))
        {
            volumeMusic = PlayerPrefs.GetFloat("volumeM");
        }
        else
        {
            volumeMusic = 1f;
        }
        if (PlayerPrefs.HasKey("volumeS"))
        {
            volumeSounds = PlayerPrefs.GetFloat("volumeS");
        }
        else
        {
            volumeSounds = 1f;
        }
        sliders[0].value = volumeMusic;
        sliders[1].value = volumeSounds;
        music.volume = volumeMusic;
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].volume = volumeSounds;
        }
    }

    private void Update()
    {
        volumeMusic = sliders[0].value;
        volumeSounds = sliders[1].value;
        music.volume = volumeMusic;
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].volume = volumeSounds;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private float Volume
    {
        get => PlayerPrefs.GetFloat("MusicVolume", 1);
        set => PlayerPrefs.SetFloat("MusicVolume", value);
    }

    private void Start()
    {
        audioSource.volume = Volume;
    }
    public float GetVolume()
    {
        return audioSource.volume;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        Volume = volume;
    }
}

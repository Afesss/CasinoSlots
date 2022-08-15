using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private float Volume
    {
        get => PlayerPrefs.GetFloat("SoundVolume", 1);
        set => PlayerPrefs.SetFloat("SoundVolume", value);
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

    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}

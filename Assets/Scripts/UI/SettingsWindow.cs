using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsWindow : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    [Inject] private Sound _sound;
    [Inject] private Music _music;

    private void OnEnable()
    {
        musicSlider.value = _music.GetVolume();
        soundSlider.value = _sound.GetVolume();
    }

    private void Update()
    {
        _music.SetVolume(musicSlider.value);
        _sound.SetVolume(soundSlider.value);
    }
}

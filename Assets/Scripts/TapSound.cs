using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TapSound : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;
    [Inject] private Sound _sound;
    public void PlaySound()
    {
        _sound.PlaySound(clickSound);
    }
}

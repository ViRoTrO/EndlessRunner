using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : BaseView
{
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        SignalService?.Subscribe<PlayAudio>(PlaySound);

    }

    private void PlaySound(PlayAudio val)
    {
       //audioSource.clip = val.audioClip;
       audioSource.PlayOneShot(val.audioClip);

    }

}

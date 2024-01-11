using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GameSO", fileName = "GameSO")]
public class GameSO : ScriptableObject
{
    [SerializeField] private List<AudioClipInfo> AudioClips;

    public AudioClip GetAudioClip(SoundNamesEnums soundType)
    {
        var info = AudioClips.Find(item => item.Id == soundType);
        if (info != null)
            return info.AudioClipPath;

        return null;
    }
}

[Serializable]
public class AudioClipInfo
{
    public SoundNamesEnums Id;

    public AudioClip AudioClipPath;
}

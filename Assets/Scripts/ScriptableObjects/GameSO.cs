using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GameSO", fileName = "GameSO")]
public class GameSO : ScriptableObject
{
    [SerializeField] private GameObject _environMentPrefab;
    [SerializeField] private List<ObstacleInfo> _obstacles;
    [SerializeField] private List<CoinsInfo> _coins;
    [SerializeField] private List<AudioClipInfo> _audioClips;

    public GameObject EnvironMentPrefab => _environMentPrefab;

    public GameObject GetObstacles(ObstacleTypesEnums obstacleId)
    {
        var info = _obstacles.Find(item => item.Id == obstacleId);
        if (info != null)
            return info.PrefabReference;

        return null;
    }

    public GameObject GetCoins(CoinTypesEnums coinId)
    {
        var info = _coins.Find(item => item.Id == coinId);
        if (info != null)
            return info.PrefabReference;

        return null;
    }

    public AudioClip GetAudioClip(SoundNamesEnums soundType)
    {
        var info = _audioClips.Find(item => item.Id == soundType);
        if (info != null)
            return info.AudioClipPath;

        return null;
    }
}

[Serializable]
public class ObstacleInfo
{
    public ObstacleTypesEnums Id;

    public GameObject PrefabReference;
}

[Serializable]
public class CoinsInfo
{
    public CoinTypesEnums Id;

    public GameObject PrefabReference;
}


[Serializable]
public class AudioClipInfo
{
    public SoundNamesEnums Id;

    public AudioClip AudioClipPath;
}

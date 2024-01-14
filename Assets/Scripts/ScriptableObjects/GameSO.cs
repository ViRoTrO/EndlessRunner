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
    [SerializeField] private float startSpeed = 5;
    [SerializeField] private float maxEnvironmentZPos = 20;
    [SerializeField] private float maxTrackItemsZPos = 5;
    [SerializeField] private float jumpPower = 7f;
    [SerializeField] private float characterSpeed = 10f;
    [SerializeField] private int maxCoinsSpawn = 30;
    [SerializeField] private int maxObstacleSpawn = 20;
    [SerializeField] private int scoreMultiplier = 100;
    [SerializeField] private int scoreTimeFactor = 5;
    [SerializeField] private int scoreDIstancFactor = 5;
    [SerializeField] private int maxLives = 3;
    [SerializeField] private float updateSpeedAfteSeconds = 10;
    [SerializeField] private float speedUpdateAmount = 5;


    public GameObject EnvironMentPrefab => _environMentPrefab;
    public float StartSpeed => startSpeed;
    public float MaxEnvironmentZPos => maxEnvironmentZPos;
    public float MaxTrackItemsZPos => maxTrackItemsZPos;
    public float JumpPower => jumpPower;
    public float CharacterSpeed => characterSpeed;
    public int MaxCoinsSpawn => maxCoinsSpawn;
    public int MaxObstacleSpawn => maxObstacleSpawn;
    public int GetObstaclesCount => _obstacles.Count;
    public int ScoreMultiplier => scoreMultiplier;
    public int ScoreDIstancFactor => scoreDIstancFactor;
    public int ScoreTimeFactor => scoreTimeFactor;
    public int MaxLives => maxLives;
    public float UpdateSpeedAfteSeconds => updateSpeedAfteSeconds;
    public float SpeedUpdateAmount => speedUpdateAmount;

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

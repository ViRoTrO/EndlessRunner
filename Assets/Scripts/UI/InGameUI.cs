using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InGameUI : BaseView
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject LivesPrefab;
    [SerializeField] private Transform LivesCOntainer;

    private List<GameObject> _livesList = new();

    public void OnPauseClicked()
    {
        SignalService.Fire(new GameStateChanged()
        {
            GameState = GameStateEnum.GamePause
        });
    }

    public void EnableUI(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    protected void Start()
    {
        SignalService.Subscribe<GameStateChanged>(OnGameStateChange);
        SignalService.Subscribe<CoinsCollected>(OnCoinCOllect);
        SignalService.Subscribe<PlayerHitObstacle>(OnPlayerHitObstacle);
        InitUI();
    }

    private void InitUI()
    {
        for (var i = 0; i < GameInfoSO.MaxLives; i++)
        {
            var go = Instantiate(LivesPrefab, LivesCOntainer);
            _livesList.Add(go);
        }
    }

    private void OnGameStateChange(GameStateChanged gameState)
    {
        if (gameState.GameState == GameStateEnum.GamePlayStart)
            gameObject.SetActive(true);
        else if (gameState.GameState == GameStateEnum.GameOver)
            InitUI();
    }

    private void OnCoinCOllect()
    {
        scoreText.text = $"{Model.CoinsCollected * GameInfoSO.ScoreMultiplier}";
    }

    private void OnPlayerHitObstacle()
    {
        if (_livesList.Count > 0)
        {
            var go = _livesList.Last();
            _livesList.Remove(go);
            Destroy(go);
        }
    }

}

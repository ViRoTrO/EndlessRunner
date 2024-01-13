

using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : BaseView
{
    [SerializeField] private MainMenuUI mainMenuUI;
    [SerializeField] private InGameUI inGameUI;
    [SerializeField] private GameObject mainMenuBackground;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform mainMenuCameraPosition;
    [SerializeField] private Transform inGameCameraPosition;

    private void Start()
    {
        Model.StartSpeed = GameInfoSO.StartSpeed;
        SignalService.Subscribe<GameStateChanged>(OnGameStateChange);
        SignalService.Subscribe<CoinsCollected>(OnCoinCOllect);
        SignalService.Subscribe<PlayerHitObstacle>(OnPlayerHitObstacle);
        AddMainMenu();
        SetInitData();
    }

    private void SetInitData()
    {
        Model.LivesRemaining = GameInfoSO.MaxLives;
        Model.CurrentScore = 0;
    }

    private void OnGameStateChange(GameStateChanged gameState)
    {
        Model.CurrentGameState = gameState.GameState;

        switch (gameState.GameState)
        {
            case GameStateEnum.GamePlayStart:
                RemoveMainMenu();
                break;

            case GameStateEnum.GamePause:
                Time.timeScale = 0;
                break;

            case GameStateEnum.GameUnPause:
                Time.timeScale = 1;
                break;
        }

    }

    private void OnCoinCOllect()
    {
        Model.CoinsCollected++;
    }

    private void OnPlayerHitObstacle()
    {
        Model.LivesRemaining--;

        if (Model.LivesRemaining < 0)
        {
            SignalService.Fire(new GameStateChanged()
            {
                GameState = GameStateEnum.GameOver
            });

            AddMainMenu();
            SetInitData();
        }
    }

    private void RemoveMainMenu()
    {
        mainMenuUI.EnableUI(false);
        inGameUI.EnableUI(true);
        mainMenuBackground.SetActive(false);
        mainCamera.gameObject.transform.position = inGameCameraPosition.position;
        mainCamera.gameObject.transform.rotation = inGameCameraPosition.rotation;
    }

    private void AddMainMenu()
    {
        mainMenuUI.EnableUI(true);
        inGameUI.EnableUI(false);
        mainMenuBackground.SetActive(true);
        mainCamera.gameObject.transform.position = mainMenuCameraPosition.position;
        mainCamera.gameObject.transform.rotation = mainMenuCameraPosition.rotation;
    }
}

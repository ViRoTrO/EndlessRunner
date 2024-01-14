using System.Collections;
using UnityEngine;

public class GameManager : BaseView
{
    [SerializeField] private MainMenuUI mainMenuUI;
    [SerializeField] private InGameUI inGameUI;
    [SerializeField] private GameObject mainMenuBackground;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform mainMenuCameraPosition;
    [SerializeField] private Transform inGameCameraPosition;
    [SerializeField] private CameraShake cameraShake;

    private void Start()
    {
        Model.SetStartSpeed(GameInfoSO.StartSpeed);
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
        Model.ScoreCoinCollectionFactor = GameInfoSO.ScoreMultiplier;
        Model.ScoreTimeFactor = GameInfoSO.ScoreMultiplier;
        Model.ScoreDistanceFactor = GameInfoSO.ScoreMultiplier;
    }

    private void OnGameStateChange(GameStateChanged gameState)
    {
        Model.CurrentGameState = gameState.GameState;

        switch (gameState.GameState)
        {
            case GameStateEnum.GamePlayStart:
                OnGameStart();
                break;

            case GameStateEnum.GamePause:
                Time.timeScale = 0;
                break;

            case GameStateEnum.GameUnPause:
                Time.timeScale = 1;
                break;
            case GameStateEnum.Playing:
                StartCoroutine(UpdateScore());
                break;
        }

    }

    private void OnGameStart()
    {
        Model.LivesRemaining = GameInfoSO.MaxLives;
        Model.Reset();
        RemoveMainMenu();
        StartCoroutine(UpdateSpeed());
        Time.timeScale = 1;
        Model.SetStartSpeed(GameInfoSO.StartSpeed);
        TrackItemBehavior.PauseTrack = false;
    }

    private void OnCoinCOllect()
    {
        Model.CoinsCollected++;
    }

    private void OnPlayerHitObstacle()
    {
        Model.LivesRemaining--;

        StartCoroutine(cameraShake.Shake(0.2f, 0.5f));

        if (Model.LivesRemaining < 0)
        {
            ShowGameOver();
            SignalService.Fire(new GameStateChanged()
            {
                GameState = GameStateEnum.GameOver
            });

        }
    }

    private void ShowGameOver()
    {
        TrackItemBehavior.PauseTrack = true;
        Model.GameOver();
        AddMainMenu();
        StopAllCoroutines();
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

    private IEnumerator UpdateSpeed()
    {
        while (0 == 0)
        {
            yield return new WaitForSeconds(GameInfoSO.UpdateSpeedAfteSeconds);
            Model.IncreaseSpeed(GameInfoSO.SpeedUpdateAmount);
        }
    }

    private IEnumerator UpdateScore()
    {
        while (Model.CurrentGameState == GameStateEnum.Playing)
        {
            yield return new WaitForSeconds(1);
            Model.UpdateScoreOnElapsedTime();
            Model.UpdateScoreOnDistaceCovered();
            SignalService.Fire(new UpdateUIScoreSignal());
        }
    }

}

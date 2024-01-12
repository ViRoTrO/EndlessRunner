using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : BaseView
{
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject unPauseButton;
    
    public void EnableUI(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    protected void Start()
    {
        SignalService.Subscribe<SwipeDetectionSignal>(SwipeDirection);
        SignalService.Subscribe<GameStateChanged>(OnGameStateChange);
        InitUI();
    }

    private void InitUI()
    {
        gameObject.SetActive(true);
        playButton.SetActive(true);
        unPauseButton.SetActive(false);
    }

    private void SwipeDirection(SwipeDetectionSignal val)
    {
        Debug.Log(val.Direction.ToString());
    }

    public void OnPlayClick()
    {
        SignalService.Fire(new GameStateChanged()
        {
            GameState = GameStateEnum.GamePlayStart
        });

        gameObject.SetActive(false);
    }

    public void OnUnPauseClick()
    {
        SignalService.Fire(new GameStateChanged()
        {
            GameState = GameStateEnum.GameUnPause
        });

        gameObject.SetActive(false);
    }


    public void OnExitClick()
    {
        Application.Quit();
    }

    private void OnGameStateChange(GameStateChanged gameState)
    {
        if(gameState.GameState == GameStateEnum.GamePause)
        {
            gameObject.SetActive(true);
            playButton.SetActive(false);
            unPauseButton.SetActive(true);
        }
        else if(gameState.GameState == GameStateEnum.GameOver)
        {
            gameObject.SetActive(true);
            playButton.SetActive(true);
            unPauseButton.SetActive(false);
        }
    }
}
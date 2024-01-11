using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : BaseView
{
    private void Start()
    {
        SignalService?.Subscribe<GameStarted>(GameStarted);
        SignalService?.Subscribe<SwipeDetectionSignal>(SwipeDirection);
    }

    private void GameStarted(GameStarted val)
    {
        Debug.Log(val.Message);
        
    }

    private void SwipeDirection(SwipeDetectionSignal val)
    {
        Debug.Log(val.Direction.ToString());
    }

    public void OnPlayClick()
    {
        SignalService.Fire(new GameStateChanged()
        {
            GameState = GameStateEnum.GameStart
        });
    }
}

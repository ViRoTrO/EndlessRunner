using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseView
{    private void Start()
    {
        SignalService.Subscribe<SwipeDetectionSignal>(SwipeDirection);
        SignalService.Subscribe<GameStateChanged>(OnGameStateChange);
    }

    private void SwipeDirection(SwipeDetectionSignal val)
    {
        Debug.Log(val.Direction.ToString());
    }

    private void OnGameStateChange(GameStateChanged gameState)
    {
        // if(gameState.GameState == GameStateEnum.Pause)
        //     gameObject.SetActive(true);

    }
}

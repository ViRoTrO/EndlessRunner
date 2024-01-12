using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : BaseView
{

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
    }

    private void OnGameStateChange(GameStateChanged gameState)
    {
        if(gameState.GameState == GameStateEnum.GamePlayStart)
            gameObject.SetActive(true);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{

    public float StartSpeed {set => _speed = value;}
    public int CurrentScore {set; get;}
    public int CoinsCollected {set; get;}
    public int HighScore {set; get;}
    public int LivesRemaining {set; get;}
    public GameStateEnum CurrentGameState {set; get;}
    public float CurrentSpeed {get => _speed * Time.deltaTime;}

    private float _speed = 5;

    private void IncreaseSpeed()
    {

    }

}

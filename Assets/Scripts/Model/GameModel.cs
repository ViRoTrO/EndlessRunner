using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{

    public float CurrentSpeed {set; get;} = 5;
    public int CurrentScore {set; get;}
    public int CoinsCollected {set; get;}
    public int HighScore {set; get;}
    public GameStateEnum CurrentGameState {set; get;}

}

using UnityEngine;

public class GameStarted
{
    public string Message = "Game started !!";
}

public class SwipeDetectionSignal
{
    public SwipeDirectionEnums Direction;
}

public class PlayAudio
{
    public AudioClip audioClip;
}

public class GameStateChanged
{
    public GameStateEnum GameState;
}
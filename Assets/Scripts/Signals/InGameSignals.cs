using UnityEngine;

public class SwipeDetectionSignal
{
    public SwipeDirectionEnums Direction;
}

public class PlayAudio
{
    public SoundNamesEnums audioClip;
}

public class PlayAudioInLoop
{
    public SoundNamesEnums audioClip;
}

public class StopAudio
{
}

public class GameStateChanged
{
    public GameStateEnum GameState;
}

public class CoinsCollected
{
}

public class UpdateUIScoreSignal
{
}

public class PlayerHitObstacle
{
}
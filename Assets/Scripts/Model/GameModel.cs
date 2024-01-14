using UnityEngine;

public class GameModel
{
    public int CurrentScore { set; get; }
    public int ScoreCoinCollectionFactor { set; get; }
    public int ScoreTimeFactor { set; get; }
    public int ScoreDistanceFactor { set; get; }
    public int CoinsCollected { set; get; }
    public int HighScore { set; get; }
    public int LivesRemaining { set; get; }
    public GameStateEnum CurrentGameState { set; get; }
    public float CurrentSpeed { get => _speed * Time.deltaTime; }

    private float _startSpeed;
    private float _speed;

    public void SetStartSpeed(float value)
    {
        _speed = value; 
        _startSpeed = value;
    }

    public void IncreaseSpeed(float val)
    {
        _speed += val;
    }

    public void Reset()
    {
        _speed = _startSpeed;
        CoinsCollected = 0;
        CurrentScore = 0;
    }

    public void UpdateScoreOnElapsedTime()
    {
        CurrentScore += ScoreTimeFactor;
    }

    public void UpdateScoreOnDistaceCovered()
    {
        CurrentScore += (int)(ScoreDistanceFactor * CurrentSpeed);
    }

    public int CalculateScoreOnCoinCOllect()
    {
        CurrentScore += ScoreCoinCollectionFactor;
        return CurrentScore;
    }

    public void GameOver()
    {
        if (CurrentScore > HighScore)
            HighScore = CurrentScore;
    }
}

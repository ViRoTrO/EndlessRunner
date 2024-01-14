using TMPro;
using UnityEngine;

public class MainMenuUI : BaseView
{
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject unPauseButton;
    [SerializeField] private TextMeshProUGUI soreText;
    [SerializeField] private GameObject scoreContainer;
    [SerializeField] private TextMeshProUGUI highSoreText;
    [SerializeField] private GameObject highScoreContainer;
    [SerializeField] private GameObject gameOverObject;


    public void EnableUI(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    protected void Start()
    {
        SignalService.Subscribe<GameStateChanged>(OnGameStateChange);
        InitUI();
    }

    private void InitUI()
    {
        gameObject.SetActive(true);
        playButton.SetActive(true);
        unPauseButton.SetActive(false);
        scoreContainer.SetActive(false);
        highScoreContainer.SetActive(false);
        gameOverObject.SetActive(false);
    }

    public void OnPlayClick()
    {
        SignalService.Fire(new GameStateChanged()
        {
            GameState = GameStateEnum.GamePlayStart
        });

        SignalService.Fire(new GameStateChanged()
        {
            GameState = GameStateEnum.Playing
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
        if (gameState.GameState == GameStateEnum.GamePause)
        {
            gameObject.SetActive(true);
            playButton.SetActive(false);
            unPauseButton.SetActive(true);
            scoreContainer.SetActive(false);
            highScoreContainer.SetActive(false);
            gameOverObject.SetActive(false);
        }
        else if (gameState.GameState == GameStateEnum.GameOver)
        {
            gameObject.SetActive(true);
            playButton.SetActive(true);
            unPauseButton.SetActive(false);
            scoreContainer.SetActive(true);
            highScoreContainer.SetActive(true);
            soreText.text = Model.CurrentScore.ToString();
            highSoreText.text = Model.HighScore.ToString();
            gameOverObject.SetActive(true);
        }
    }
}
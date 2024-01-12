

using Unity.VisualScripting;
using UnityEngine;

public class GameManager : BaseView
{
    [SerializeField] private MainMenuUI mainMenuUI;
    [SerializeField] private InGameUI inGameUI;
    [SerializeField] private GameObject mainMenuBackground;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform mainMenuCameraPosition;
    [SerializeField] private Transform inGameCameraPosition;

    private void Start()
    {
        SignalService.Subscribe<GameStateChanged>(OnGameStateChange);
    }

    private void OnGameStateChange(GameStateChanged gameState)
    {
        Model.CurrentGameState = gameState.GameState;
        
        switch(gameState.GameState)
        {
            case GameStateEnum.GamePlayStart:
                RemoveMainMenu();
            break;

            case GameStateEnum.GamePause:
                Time.timeScale = 0;
            break;

            case GameStateEnum.GameUnPause:
                Time.timeScale = 1;
            break;
        }

    }

    private void RemoveMainMenu()
    {
        mainMenuUI.EnableUI(false);
        inGameUI.EnableUI(true);
        mainMenuBackground.SetActive(false);
        mainCamera.gameObject.transform.position = inGameCameraPosition.position;
        mainCamera.gameObject.transform.rotation = inGameCameraPosition.rotation;
    }

    private void AddMainMenu()
    {
        mainMenuUI.EnableUI(true);
        inGameUI.EnableUI(false);
        mainMenuBackground.SetActive(true);
        mainCamera.gameObject.transform.position = mainMenuCameraPosition.position;
        mainCamera.gameObject.transform.rotation = mainMenuCameraPosition.rotation;
    }
}

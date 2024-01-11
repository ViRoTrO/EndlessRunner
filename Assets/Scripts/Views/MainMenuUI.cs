using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : BaseView
{

    // Start is called before the first frame update
    void Start()
    {
        SignalService?.Subscribe<GameStarted>(GameStarted);
    }

    private void GameStarted(GameStarted val)
    {
        Debug.Log(val.Message);

    }

    public void OnPlayClick()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GanerateAndAddEnvironment : BaseView
{
    [SerializeField] private SpawnerPool spawner;
    [SerializeField] private int maxSpawnCount = 3;

    private List<GameObject> environmentList = new List<GameObject>();

    protected void Start()
    {
        SignalService.Subscribe<GameStateChanged>(OnGameStateChange);
    }

    private void OnGameStateChange(GameStateChanged gameState)
    {
        if (gameState.GameState == GameStateEnum.GamePause)
        {

        }
        else if (gameState.GameState == GameStateEnum.GameOver)
        {

        }
    }

    protected void Update()
    {
        if (Model.CurrentGameState == GameStateEnum.GamePlayStart && environmentList.Count < maxSpawnCount)
        {
            var startFrom = environmentList.Count == 0 ? 0 : environmentList.Count - 1;
            for (int i = startFrom; i < maxSpawnCount; i++)
            {
                var go = spawner.GetEnvironment();
                environmentList.Add(go);
                if (i > 0)
                {
                    var previousGO = environmentList[i - 1];
                    var position = previousGO.transform.position;
                    var zpos = previousGO.GetComponent<BoxCollider>().bounds.size.z;
                    go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, zpos);
                }
            }
        }
    }

}

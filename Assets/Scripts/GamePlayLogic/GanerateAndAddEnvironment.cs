using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
        if (gameState.GameState == GameStateEnum.GamePlayStart)
        {
            InitiateEnvironments();
        }
        else if (gameState.GameState == GameStateEnum.GameOver)
        {

        }
    }

    private void InitiateEnvironments()
    {
        for (int i = 0; i < maxSpawnCount; i++)
        {
            var go = CreateItem();
            PositionItem(go.gameObject);
            environmentList.Add(go.gameObject);
        }
    }

    private TrackItemBehavior CreateItem()
    {
        var go = spawner.GetEnvironment();
        var trackItem = go.GetComponent<TrackItemBehavior>();
        trackItem.SetModel(Model, GameInfoSO.MaxEnvironmentZPos);
        trackItem.OnReachMaxDistanceEvent += OnDestinationReached;

        return trackItem;
    }

    private void PositionItem(GameObject go)
    {
        if(environmentList.Count == 0) return;

        var previousGO = environmentList.Last();
        var position = previousGO.transform.position;
        var zpos = previousGO.GetComponent<BoxCollider>().bounds.size.z;
        go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, position.z + zpos);       
    }

    private void ReleaseItem(GameObject item)
    {
        spawner.ReleaseEnvironment(item);
        environmentList.Remove(item);
    }

    private void OnDestinationReached(TrackItemBehavior trackItem)
    {
        trackItem.OnReachMaxDistanceEvent -= OnDestinationReached;
        ReleaseItem(trackItem.gameObject);

        var go = CreateItem();
        PositionItem(go.gameObject);
        environmentList.Add(go.gameObject);
    }

}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateCoins : BaseView
{
    [SerializeField] private SpawnerPool spawner;
    [SerializeField] private int coinSpawnDistance;
    [SerializeField] private int startDistance = 40;
    [SerializeField] private int obstacleSpawnDistance;
    [SerializeField] private int minCoinTrail = 5;
    [SerializeField] private int maxCoinTrail = 10;
    [SerializeField] private Transform coinSpawnRightPosition;
    [SerializeField] private Transform coinSpawnCenterPosition;
    [SerializeField] private Transform coinSpawnLeftPosition;




    private List<GameObject> _coinList = new List<GameObject>();
    private List<GameObject> _obstacleList = new List<GameObject>();
    private int _currentCoinTrail;
    private Transform _currentCoinSpanPosition;

    protected void Start()
    {
        SignalService.Subscribe<GameStateChanged>(OnGameStateChange);
    }

    private void OnGameStateChange(GameStateChanged gameState)
    {
        if (gameState.GameState == GameStateEnum.GamePlayStart)
        {
            InitiateCoins();
            InitiateObstacles();
        }
        else if (gameState.GameState == GameStateEnum.GameOver)
        {

        }
    }

    #region Coins
    private void InitiateCoins()
    {
        for (int i = 0; i < GameInfoSO.MaxCoinsSpawn; i++)
        {
            var go = CreateCoin();
            PositionCoin(go.gameObject);
            _coinList.Add(go.gameObject);
        }
    }

    private TrackItemBehavior CreateCoin()
    {
        if (_currentCoinTrail <= 0)
        {
            _currentCoinTrail = Random.Range(minCoinTrail, maxCoinTrail);
            var randomPos = Random.Range(1, 4);

            switch (randomPos)
            {
                case 1:
                    _currentCoinSpanPosition = coinSpawnRightPosition;
                    break;

                case 2:
                    _currentCoinSpanPosition = coinSpawnCenterPosition;
                    break;

                case 3:
                    _currentCoinSpanPosition = coinSpawnLeftPosition;
                    break;
            }
        }

        var go = spawner.GetCoinView();
        var trackItem = go.GetComponent<TrackItemBehavior>();
        trackItem.SetModel(Model, GameInfoSO.MaxTrackItemsZPos);
        trackItem.OnReachMaxDistanceEvent += OnCoinDestinationReached;
        go.OnCollect += OnCoinCollected;
        _currentCoinTrail--;

        return trackItem;
    }

    private void PositionCoin(GameObject go)
    {
        if (_coinList.Count == 0)
        {
            var pos = go.transform.position;
            go.transform.position = new Vector3(pos.x, pos.y, pos.z + startDistance);
            return;
        }

        var previousGO = _coinList.Last();
        var zpos = previousGO.transform.position.z + coinSpawnDistance;
        go.transform.position = new Vector3(_currentCoinSpanPosition.position.x, go.transform.position.y, zpos);
    }

    private void ReleaseCoin(GameObject item)
    {
        var coin = item.GetComponent<CoinView>();
        spawner.ReleaseCoin(coin);
        _coinList.Remove(item);
    }

    private void OnCoinDestinationReached(TrackItemBehavior trackItem)
    {
        trackItem.OnReachMaxDistanceEvent -= OnCoinDestinationReached;
        ReleaseCoin(trackItem.gameObject);

        var go = CreateCoin();
        PositionCoin(go.gameObject);
        _coinList.Add(go.gameObject);
    }

    private void OnCoinCollected(CoinView coin)
    {
        coin.OnCollect -= OnCoinCollected;
        ReleaseCoin(coin.gameObject);

        var go = CreateCoin();
        PositionCoin(go.gameObject);
        _coinList.Add(go.gameObject);
    }

    #endregion


    #region Obstcle

    private void InitiateObstacles()
    {
        for (int i = 0; i < GameInfoSO.MaxObstacleSpawn; i++)
        {
            var go = CreateObstacles();
            PositionObstacles(go.gameObject);
            _obstacleList.Add(go.gameObject);
        }
    }

    private TrackItemBehavior CreateObstacles()
    {
        var go = spawner.GetRandomObstacle();
        var trackItem = go.GetComponent<TrackItemBehavior>();
        trackItem.SetModel(Model, GameInfoSO.MaxTrackItemsZPos);
        trackItem.OnReachMaxDistanceEvent += OnObstacleDestinationReached;

        return trackItem;
    }

    private void PositionObstacles(GameObject go)
    {
        if (_obstacleList.Count == 0)
        {
            var pos = go.transform.position;
            go.transform.position = new Vector3(pos.x, pos.y, pos.z + startDistance);
            return;
        }

        var previousGO = _obstacleList.Last();
        var zpos = previousGO.transform.position.z + obstacleSpawnDistance;
        go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, zpos);
    }

    private void ReleaseObstacles(GameObject item)
    {
        var obstacle = item.GetComponent<Obstacles>();
        spawner.ReleaseObstacle(obstacle);
        _obstacleList.Remove(item);
    }

    private void OnObstacleDestinationReached(TrackItemBehavior trackItem)
    {
        trackItem.OnReachMaxDistanceEvent -= OnObstacleDestinationReached;
        ReleaseObstacles(trackItem.gameObject);

        var go = CreateObstacles();
        PositionObstacles(go.gameObject);
        _obstacleList.Add(go.gameObject);
    }

    #endregion
}

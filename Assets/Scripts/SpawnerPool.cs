using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerPool : BaseView
{
    [SerializeField] private int ObstacleMaxPoolSize = 20;
    [SerializeField] private int CoinsMaxPoolSize = 30;
    [SerializeField] private int EnvironmentPoolSize = 10;
    [SerializeField] private Transform SpawnContainer;



    private Dictionary<ObstacleTypesEnums, ObjectPool<Obstacles>> _obstaclePool;
    private ObjectPool<CoinView> _coinsPool;
    private ObjectPool<GameObject> _environmentPool;
    private GameObject _currentObstaclePrefab;

    public CoinView GetCoinView()
    {
        if (_coinsPool == null)
            _coinsPool = new ObjectPool<CoinView>(CreatePooledCoinItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, 10, CoinsMaxPoolSize);

        return _coinsPool.Get();
    }

    public Obstacles GetRandomObstacle()
    {
        var random = Random.Range(0, GameInfoSO.GetObstaclesCount);
        var en = (ObstacleTypesEnums)random;
        return GetObstacleView(en);
    }

    public Obstacles GetObstacleView(ObstacleTypesEnums type)
    {
        if (_obstaclePool == null)
            _obstaclePool = new Dictionary<ObstacleTypesEnums, ObjectPool<Obstacles>>();

        if (!_obstaclePool.ContainsKey(type))
            _obstaclePool[type] = new ObjectPool<Obstacles>(CreateObstacle, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, 10, ObstacleMaxPoolSize);

        _currentObstaclePrefab = GameInfoSO.GetObstacles(type);
        return _obstaclePool[type].Get();
    }

    public GameObject GetEnvironment()
    {
        if (_environmentPool == null)
            _environmentPool = new ObjectPool<GameObject>(CreatePooledEnvironment, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, 10, EnvironmentPoolSize);

        return _environmentPool.Get();
    }

    public void ReleaseCoin(CoinView item) => _coinsPool.Release(item);

    public void ReleaseObstacle(Obstacles item)
    {
        if(item.gameObject.activeInHierarchy)
            _obstaclePool[item.Id].Release(item);
    } 
    public void ReleaseEnvironment(GameObject item) => _environmentPool.Release(item);


    private Obstacles CreateObstacle()
    {
        var go = Instantiate(_currentObstaclePrefab, SpawnContainer);
        go.TryGetComponent<Obstacles>(out var obstacle);

        return obstacle;
    }

    private CoinView CreatePooledCoinItem()
    {
        var prefab = GameInfoSO.GetCoins(CoinTypesEnums.Noraml);
        var go = Instantiate(prefab, SpawnContainer);
        go.TryGetComponent<CoinView>(out var component);

        return component;
    }

    private GameObject CreatePooledEnvironment()
    {
        var prefab = GameInfoSO.EnvironMentPrefab;
        var go = Instantiate(prefab, SpawnContainer);
        return go;
    }

    private void OnReturnedToPool(CoinView system) => system.gameObject.SetActive(false);

    private void OnTakeFromPool(CoinView item) => item.gameObject.SetActive(true);

    private void OnDestroyPoolObject(CoinView item) => Destroy(item.gameObject);

    private void OnReturnedToPool(Obstacles system) => system.gameObject.SetActive(false);

    private void OnTakeFromPool(Obstacles item) => item.gameObject.SetActive(true);

    private void OnDestroyPoolObject(Obstacles item) => Destroy(item.gameObject);

    private void OnReturnedToPool(GameObject item) => item.gameObject.SetActive(false);

    private void OnTakeFromPool(GameObject item) => item.gameObject.SetActive(true);

    private void OnDestroyPoolObject(GameObject item) => Destroy(item.gameObject);


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BaseView : MonoBehaviour
{
    [SerializeField] protected GameSO GameInfoSO;

    protected GameModel Model;
    protected SignalBus SignalService;

    [Inject]
    private void Init(GameModel gameModel, SignalBus signalBus)
    {
        Model = gameModel;
        SignalService = signalBus;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseView
{
    private void OnEnable()
    {
        SignalService.Fire(new GameStarted());
    }
}

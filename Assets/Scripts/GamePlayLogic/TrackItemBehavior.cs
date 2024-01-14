using System;
using UnityEngine;

public class TrackItemBehavior : MonoBehaviour
{
    public static bool PauseTrack;
    public event Action<TrackItemBehavior> OnReachMaxDistanceEvent;

    private GameModel _model;
    private float _maxDistance;
    private bool _isReachMaxDistance;

    public void SetModel(GameModel model, float maxDistance)
    {
        _model = model;
        _maxDistance = maxDistance;
        _isReachMaxDistance = false;
    }

    private void Update()
    {
        if (PauseTrack) return;

        if (_model != null && !_isReachMaxDistance)
        {
            gameObject.transform.position += Vector3.back * _model.CurrentSpeed;

            if (gameObject.transform.position.z < _maxDistance)
            {
                _isReachMaxDistance = true;
                OnReachMaxDistanceEvent.Invoke(this);
            }

        }

    }
}

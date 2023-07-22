using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ReleaseAir : MonoBehaviour
{
    [SerializeField] private TimerHold _timerHold;
    [SerializeField] private DeflationGameLogic _gameLogic;
    [SerializeField] private float _rateOfAirRemoval = 1.0f;
    [SerializeField, Range(0, 1)] private float _minimumAirValue = 0.1f;
    private float _airbagValue = 0.5f;
    private CompositeDisposable _cd;
    
    public void SetAirBagValue()
    {
        _gameLogic.GetCurrentPatient().StomaBagAirValue.Value.Subscribe(val =>
        {
            _airbagValue = val;
        }).AddTo(_cd);
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        _cd = new CompositeDisposable();
        SetAirBagValue();
        _timerHold.OnAcceptableRange.AddListener(delegate { ReleaseAirBagValue(); });
        _timerHold.OnLateRange.AddListener(delegate {});
    }

    // Update is called once per frame
    void OnDisable()
    {
        _timerHold.OnAcceptableRange.RemoveAllListeners();
        _timerHold.OnLateRange.RemoveAllListeners();
        _cd.Dispose();
    }

    private void ReleaseAirBagValue()
    {
        _airbagValue -= (Time.deltaTime / _rateOfAirRemoval);
        Debug.Log(_airbagValue);
        if (_airbagValue < _minimumAirValue && DeflationTaskController.Instance.GetCurrentTask() == DeflationTasks.RELEASE_AIR)
        {
            DeflationTaskController.Instance.MarkCurrentTaskAsDone();
            _gameLogic.GetCurrentPatient().StomaBagAirValue.SetValue(_airbagValue);
        }
    }
}

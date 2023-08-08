using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Events;


public class RealseAirForMultipleBag : MonoBehaviour
{
    [SerializeField] private TimerHold _timerHold;
    [SerializeField] private DeflationGameLogic _gameLogic;
    [SerializeField] private float _rateOfAirRemoval = 1.0f;
    [SerializeField, Range(0, 1)] private float _minimumAirValue = 0.1f;
    private float _airbagValue = 0.5f;
    private CompositeDisposable _cd;
    Bag[] bags;
    public void SetAirBagValue()
    {

    }

    // Start is called before the first frame update
    void OnEnable()
    {
        bags = FindObjectsOfType<Bag>();
        _cd = new CompositeDisposable();
        SetAirBagValue();
        _timerHold.OnAcceptableRange.AddListener(delegate { ReleaseAirBagValue(); });
        _timerHold.OnLateRange.AddListener(delegate { });
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
        Bag[] bags = FindObjectsOfType<Bag>();
        for (int i = 0; i < bags.Length; i++)
        {
            if (bags[i].Intereced)
            {
                bags[i].SetAirBagValue(-(Time.deltaTime / _rateOfAirRemoval));
                _airbagValue = bags[i].GetAirBagValue();
                if (bags[i].GetAirBagValue() < _minimumAirValue && DeflationTaskController.Instance.GetCurrentTask() == DeflationTasks.RELEASE_AIR)
                {
                    DeflationTaskController.Instance.MarkCurrentTaskAsDone();
                    bags[i].Intereced = false;
                }
            }
        }
    }
}

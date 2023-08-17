using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ReleaseAir : BasicSlider
{
    [SerializeField, Range(0, 1)] private float _maxmimumSlideValue = 0.2f; 
    private float _previousValue = 0;

    // Start is called before the first frame update
    void OnEnable()
    {
        _mainSlider.onValueChanged.AddListener(delegate { ReleaseAirBagValue(); });
    }

    // Update is called once per frame
    void OnDisable()
    {
        _mainSlider.onValueChanged.RemoveAllListeners();
    }

    private void ReleaseAirBagValue()
    {
        if (Mathf.Abs(_mainSlider.value - _previousValue) >= _maxmimumSlideValue)
        {
            Debug.Log("Woah");   
        }

        if (_mainSlider.value >= _sliderPassReq && DeflationProcedureTaskController.Instance.GetCurrentTask() == DeflationProcedureTasks.RELEASE_AIR)
        {
            DeflationProcedureTaskController.Instance.MarkCurrentTaskAsDone();
        }

        _previousValue = _mainSlider.value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

public class RollerClamp : TwoWaySlider
{
    private static float _rollerClampValue = 0;

    private void OnDisable()
    {
        _mainSlider.onValueChanged.RemoveAllListeners();
        _rollerClampValue = _mainSlider.value;
    }

    private void OnEnable()
    {
        if (PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.CLAMP_ROLLER_CLAMP)
        {
            _mainSlider.onValueChanged.AddListener(delegate { SetSliderClose(); });
        }
        else if (PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.OPEN_ROLLER_CLAMP)
        {
            _mainSlider.onValueChanged.AddListener(delegate { SetSliderOpen(); });
        }
        _mainSlider.value = _rollerClampValue;
    }

    private void SetSliderClose()
    {
        if (_mainSlider.value >= _sliderOppPassReq && PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.CLAMP_ROLLER_CLAMP)
        {
            // Good enough, mark as pass and move on
            PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();
            _sliderOppPassEvent.Invoke();
            _mainSlider.onValueChanged.RemoveListener(delegate { SetSliderClose(); });
        }

    }

    private void SetSliderOpen()
    {
        
        // Good enough, mark as pass and move on
        if (_mainSlider.value <= _sliderPassReq && PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.OPEN_ROLLER_CLAMP)
        {
            PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();
            _sliderPassEvent.Invoke();
            _mainSlider.onValueChanged.RemoveListener(delegate { SetSliderOpen(); });
        }

    }
}

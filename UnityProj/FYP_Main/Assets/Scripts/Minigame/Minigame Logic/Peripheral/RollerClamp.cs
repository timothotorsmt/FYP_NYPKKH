using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RollerClamp : SliderAction
{
    private static float _rollerClampValue = 0;
    [SerializeField, Range(0,1)] private float _reqToOpenPass = 0.2f;

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveAllListeners();
        _rollerClampValue = _slider.value;
    }

    private void OnEnable()
    {
        if (PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.CLAMP_ROLLER_CLAMP)
        {
            _slider.onValueChanged.AddListener(delegate { SetSliderClose(); });
        }
        else if (PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.OPEN_ROLLER_CLAMP)
        {
            _slider.onValueChanged.AddListener(delegate { SetSliderOpen(); });
        }
        _slider.value = _rollerClampValue;
    }

    private void SetSliderClose()
    {
        if (_slider.value >= _reqToPass && PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.CLAMP_ROLLER_CLAMP)
        {
            // Good enough, mark as pass and move on
            PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();
            _sliderPassEvent.Invoke();
            _slider.onValueChanged.RemoveListener(delegate { SetSliderClose(); });
        }

    }

    private void SetSliderOpen()
    {
        if (_slider.value <= _reqToOpenPass && PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.OPEN_ROLLER_CLAMP)
        {
            // Good enough, mark as pass and move on
            PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();
            _sliderPassEvent.Invoke();
            _slider.onValueChanged.RemoveListener(delegate { SetSliderOpen(); });
        }

    }
}

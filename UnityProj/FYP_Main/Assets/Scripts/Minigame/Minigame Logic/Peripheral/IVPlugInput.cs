using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IVPlugInput : SliderAction
{

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveAllListeners();
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(delegate { SetSliderClose(); });
    }

    private void SetSliderClose()
    {
        if (_slider.value >= _reqToPass && PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.CONNECT_INFUSION_TUBING)
        {
            // Good enough, mark as pass and move on
            PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();
            _sliderPassEvent.Invoke();
            _slider.onValueChanged.RemoveListener(delegate { SetSliderClose(); });
        }

    }
}

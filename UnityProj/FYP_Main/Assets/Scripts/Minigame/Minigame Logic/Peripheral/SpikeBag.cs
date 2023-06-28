using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class SpikeBag : SliderAction
{

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveAllListeners();
    }

    private void OnEnable()
    {
        if (PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.SPIKE_INFUSION_BOTTLE)
        {
            _slider.onValueChanged.AddListener(delegate { SetSliderClose(); });
        }

    }

    private void SetSliderClose()
    {
        if (_slider.value >= _reqToPass && PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.SPIKE_INFUSION_BOTTLE)
        {
            // Good enough, mark as pass and move on
            PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();
            _slider.interactable = false;
            _sliderPassEvent.Invoke();
            _slider.onValueChanged.RemoveListener(delegate { SetSliderClose(); });
        }

    }
}

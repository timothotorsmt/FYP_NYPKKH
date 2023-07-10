using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class SpikeBag : BasicSlider
{
    private void OnDisable()
    {
        _mainSlider.onValueChanged.RemoveAllListeners();
    }

    private void OnEnable()
    {
        if (PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.SPIKE_INFUSION_BOTTLE)
        {
            _mainSlider.onValueChanged.AddListener(delegate { SetSliderClose(); });
        }

    }

    private void SetSliderClose()
    {
        if (_mainSlider.value >= _sliderPassReq && PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.SPIKE_INFUSION_BOTTLE)
        {
            // Good enough, mark as pass and move on
            PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();

            _mainSlider.interactable = false;
            _sliderPassEvent.Invoke();
            _mainSlider.onValueChanged.RemoveListener(delegate { SetSliderClose(); });
        }

    }
}

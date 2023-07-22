using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : BasicSlider
{

    private void OnDisable()
    {
        _mainSlider.onValueChanged.RemoveAllListeners();
    }

    private void OnEnable()
    {
        _mainSlider.onValueChanged.AddListener(delegate { SetSliderComplete(); });
    }

    private void SetSliderComplete()
    {
        if (_mainSlider.value >= _sliderPassReq && PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.CLOSE_DOOR)
        {
            // Good enough, mark as pass and move on
            PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();
            _sliderPassEvent.Invoke();
            _mainSlider.interactable = false;
            _mainSlider.onValueChanged.RemoveListener(delegate { SetSliderComplete(); });
        }


    }
}

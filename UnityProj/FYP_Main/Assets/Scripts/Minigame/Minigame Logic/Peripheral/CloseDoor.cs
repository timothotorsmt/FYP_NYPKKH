using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : SliderAction
{

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveAllListeners();
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(delegate { SetSliderComplete(); });
    }

    private void SetSliderComplete()
    {
        if (_slider.value >= _reqToPass && PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.CLOSE_DOOR)
        {
            // Good enough, mark as pass and move on
            PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();
            _sliderPassEvent.Invoke();
            _slider.interactable = false;
            _slider.onValueChanged.RemoveListener(delegate { SetSliderComplete(); });
        }


    }
}

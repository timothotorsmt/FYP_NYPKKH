using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBag : BasicSlider
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
        if (_mainSlider.value >= _sliderPassReq && DeflationProcedureTaskController.Instance.GetCurrentTask() == DeflationProcedureTasks.ROLL_BAG)
        {
            // Good enough, mark as pass and move on
            DeflationProcedureTaskController.Instance.MarkCurrentTaskAsDone();
            _sliderPassEvent.Invoke();
            _mainSlider.onValueChanged.RemoveListener(delegate { SetSliderComplete(); });
        }
    }

    public void Reset()
    {
        _mainSlider.value = 0;
    }
}

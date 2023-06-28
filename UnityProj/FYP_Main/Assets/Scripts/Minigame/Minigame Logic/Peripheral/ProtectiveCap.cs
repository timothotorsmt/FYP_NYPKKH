using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ProtectiveCap : SliderAction
{
    [SerializeField] private GameObject _cap;


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
        if (_slider.value >= _reqToPass && PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.REMOVE_PROTECTIVE_CAP)
        {
            // Good enough, mark as pass and move on
            PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();
            _slider.interactable = false;
            _cap.GetComponent<Image>().DOFade(0, 1.0f);
            _sliderPassEvent.Invoke();
            _slider.onValueChanged.RemoveListener(delegate { SetSliderClose(); });
        }

    }
}

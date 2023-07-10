using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ProtectiveCap : TwoWaySlider
{
    [SerializeField] private GameObject _cap;


    private void OnDisable()
    {
        _mainSlider.onValueChanged.RemoveAllListeners();
    }

    private void OnEnable()
    {
        _mainSlider.onValueChanged.AddListener(delegate { SetSliderClose(); });
    }

    private void SetSliderClose()
    {
        if (_mainSlider.value >= _sliderPassReq && PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.REMOVE_PROTECTIVE_CAP)
        {
            // Good enough, mark as pass and move on
            PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();
            _mainSlider.interactable = false;
            _cap.GetComponent<Image>().DOFade(0, 1.0f);
            _sliderPassEvent.Invoke();
            _mainSlider.onValueChanged.RemoveListener(delegate { SetSliderClose(); });
        }

    }
}

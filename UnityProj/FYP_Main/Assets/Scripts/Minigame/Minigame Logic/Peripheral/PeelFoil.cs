using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PeelFoil : OneWaySlider
{
    // Add any extra variables
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _foil;

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
        if (_mainSlider.value >= _sliderPassReq && PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.PEEL_OFF_FOIL)
        {
            // Good enough, mark as pass and move on
            
            PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();
            DisableSlider(true);
            _sliderPassEvent.Invoke();
            _foil.GetComponent<Image>().DOFade(0, 1.0f);
            _mainSlider.onValueChanged.RemoveListener(delegate { SetSliderComplete(); });
        }

        _animator.SetFloat("Slider", _mainSlider.value);

    }


}

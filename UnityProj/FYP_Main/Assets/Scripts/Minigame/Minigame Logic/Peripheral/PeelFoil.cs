using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PeelFoil : SliderAction
{
    // Add any extra variables
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _foil;

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
        if (_slider.value >= _reqToPass && PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.PEEL_OFF_FOIL)
        {
            // Good enough, mark as pass and move on
            
            PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();
            _sliderPassEvent.Invoke();
            _slider.interactable = false;
            // Fade the image out
            _slider.gameObject.SetActive(false);
            _foil.GetComponent<Image>().DOFade(0, 1.0f);
            _slider.onValueChanged.RemoveListener(delegate { SetSliderComplete(); });
        }

        _animator.SetFloat("Slider", _slider.value);

    }


}

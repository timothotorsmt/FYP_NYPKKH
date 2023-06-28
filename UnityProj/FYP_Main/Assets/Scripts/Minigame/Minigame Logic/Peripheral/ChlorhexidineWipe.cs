using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChlorhexidineWipe : SliderAction
{
    // Add any extra variables
    [SerializeField] private GameObject _wipe;
    [SerializeField] private int _amtSwipes = 5;
    private int _swipeCount = 0;
    private bool _directionFlag = false;

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveAllListeners();
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(delegate { SetSliderComplete(); });
        _swipeCount = 0;
    }

    private void SetSliderComplete()
    {
        if (PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.CLEAN_WITH_SWAB)
        {
            if (_directionFlag)
            {
                if (_slider.value > 0.5f)
                {
                    _directionFlag = false;
                }
            }
            else
            {
                if (_slider.value < 0.5f)
                {
                    _directionFlag = true;
                    _swipeCount++;
                }
            }

            if (_swipeCount > _amtSwipes)
            {
                // Skip the unpacking of the IV tube right now 
                // TODO: resolve this
                PeripheralSetupTaskController.Instance.AssignNextTaskContinuous(PeripheralSetupTasks.CLAMP_ROLLER_CLAMP);
                PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();
                _sliderPassEvent.Invoke();
                _slider.interactable = false;
                _wipe.GetComponent<Image>().DOFade(0, 1.0f);
                _slider.onValueChanged.RemoveListener(delegate { SetSliderComplete(); });

            }
        }
    }
}

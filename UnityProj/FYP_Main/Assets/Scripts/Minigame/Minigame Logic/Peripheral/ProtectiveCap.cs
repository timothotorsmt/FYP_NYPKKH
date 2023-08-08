using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

// The protective cap on the IV tubing 
public class ProtectiveCap : BasicSlider
{
    [SerializeField] private GameObject _cap; // The cap object
    private bool pass = false;

     private void Awake()
    {
        // Subscribe to the existing thing
        PeripheralSetupTaskController.Instance.CurrentTask.Value.Subscribe(state => {
            CheckForAsync(state);
        });

     }

    private void CheckForAsync(PeripheralSetupTasks state)
    {
        switch (state)
        {
            case PeripheralSetupTasks.REMOVE_PROTECTIVE_CAP:
                if (pass)
                {
                    // Good enough, mark as pass and move on
                    PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();
                    ChatGetter.Instance.StartChat("#PERIIC");
                }
                break;
        }
    }

    private void OnDisable()
    {
        _mainSlider.onValueChanged.RemoveAllListeners();
    }

    private void OnEnable()
    {
        _mainSlider.onValueChanged.AddListener(delegate { SetSliderClose(); });
        CheckForAsync(PeripheralSetupTaskController.Instance.GetCurrentTask());
    }

    private void SetSliderClose()
    {
        if (_mainSlider.value >= _sliderPassReq)
        {
            pass = true;
            CheckForAsync(PeripheralSetupTaskController.Instance.GetCurrentTask());

            PeripheralSetupTaskController.Instance.MarkCorrectTask();
            // Good enough, mark as pass and move on
            _mainSlider.interactable = false;
            // Cap fade out
            _cap.GetComponent<Image>().DOFade(0, 1.0f);
            _sliderPassEvent.Invoke();
            _mainSlider.onValueChanged.RemoveListener(delegate { SetSliderClose(); });
        }

    }
}

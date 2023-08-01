using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UniRx;

public class SyringeFill : BasicSlider
{
    [SerializeField] private TimerHold _timerHold;
    [SerializeField, Min(0)] private float _pressDuration = 3.0f;
    private float _pressTimer;
    private bool _isPress;


    private void Start()
    {
        _timerHold.IndicatorValue.Value.Subscribe(value => {
            _mainSlider.value = value;
        });

        _timerHold.Interactable = false;
        PeripheralSetupTaskController.Instance.CurrentTask.Value.Subscribe(state =>
        {
            if (state == PeripheralSetupTasks.FLUSH_IV_PLUG_CLAVE)
            {
                _timerHold.Interactable = true;
            }
            else 
            {
                _timerHold.Interactable = false;
            }
        });
    }

    public void CompleteTask()
    {
        if (PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.FLUSH_IV_PLUG_CLAVE)
        {
            // mark as done and move tf on!!
            PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();

            _sliderPassEvent.Invoke();
        }
    }
    
}

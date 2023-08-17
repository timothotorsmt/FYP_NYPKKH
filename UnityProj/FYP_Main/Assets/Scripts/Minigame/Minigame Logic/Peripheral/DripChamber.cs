using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UniRx;

public class DripChamber : BasicSlider
{
    [SerializeField] private TimerHold _timerHold;
    [SerializeField] private Sprite _waterLevelSprite;
    [SerializeField] private Sprite _waterLevelBubbleSprite;
    [SerializeField] private Image _waterSprite;
    [SerializeField] private UnityEvent _bubbleOccurEvent;

    private bool _hasBubbles;
    private int _dripChamberCount;

    private void Start()
    {
        _mainSlider.value = 0;
        if (Random.Range(0, 2) < 1)
        {
            _hasBubbles = true;
            _waterSprite.sprite = _waterLevelBubbleSprite;
        }
        else
        {
            _hasBubbles = false;
            _waterSprite.sprite = _waterLevelSprite;
        }

        _timerHold.IndicatorValue.Value.Subscribe(value => {
            _mainSlider.value = value;
        });

        _timerHold.Interactable = false;
        PeripheralSetupTaskController.Instance.CurrentTask.Value.Subscribe(state =>
        {
            if (state == PeripheralSetupTasks.PRIME_INFUSION_TUBING)
            {
                _timerHold.Interactable = true;
            }
            else 
            {
                _timerHold.Interactable = false;
            }
        });
    }

    public void DripChamberBubbles()
    {
        if (PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.FLICK_LINE)
        {
            _dripChamberCount++;
            if (_dripChamberCount >= 3)
            {
                // okay bubbles gone
                _waterSprite.sprite = _waterLevelSprite;
                PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();
                _sliderPassEvent.Invoke();
            }
        }
    }

    public void CompleteTask()
    {
        if (PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.PRIME_INFUSION_TUBING)
        {
            // mark as done and move tf on!!
            PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();

            if (_hasBubbles)
            {
                PeripheralSetupTaskController.Instance.AssignTasks(PeripheralSetupTasks.FLICK_LINE);
                _dripChamberCount = 0;
                _bubbleOccurEvent.Invoke();
            }
            else
            {
                _sliderPassEvent.Invoke();
            }
        }
    }
}

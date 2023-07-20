using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DripChamber : SliderAction
{
    [SerializeField] private Sprite _waterLevelSprite;
    [SerializeField] private Sprite _waterLevelBubbleSprite;
    [SerializeField] private Image _waterSprite;
    [SerializeField] private float _holdDuration = 3.0f;
    [SerializeField] private UnityEvent _bubbleOccurEvent;

    private float _raiseAmount;
    private bool _hasBubbles;
    private bool _isBeingPressed;
    private int _dripChamberCount;

    private void Start()
    {
        _slider.value = 0;
        _raiseAmount = _reqToPass / _holdDuration;
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

    }

    private void Update()
    {
        if (_isBeingPressed) { PressBag(); }
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

    private void PressBag()
    {
        // Presses the bag..  raise the water level
        if (PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.PRIME_INFUSION_TUBING)
        {
            _slider.value += (Time.deltaTime / _holdDuration);
            if (_slider.value > _reqToPass)
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
        else
        {
            // why they overflowing the bag.. put voice line
        }
    }

    public void StartPress() { Debug.Log("Huh"); _isBeingPressed = true; }
    public void EndPress() { _isBeingPressed = false; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using Core.Input;
using UniRx.Extention;
using UniRx;

// This class controls the main functionality for the squeezing of the drip chamber 
// To allow the drip chamber to fill up

public class SqueezeBag : MonoBehaviour
{
    [SerializeField] private Slider _waterLevel;
    [SerializeField] private Sprite _waterLevelSprite;
    [SerializeField] private Sprite _waterLevelBubbleSprite;
    [SerializeField] private Image _waterSprite;
    [SerializeField] private float _holdDuration = 3.0f;
    [SerializeField, Range(0,1)] private float _completionPercent = 0.4f;
    [SerializeField, Range(0,1)] private float _maxCompletionPercent = 0.6f;
    [SerializeField] private UnityEvent _bubbleOccurEvent;
    [SerializeField] private UnityEvent _finishTaskEvent;

    private float _raiseAmount;
    private bool _hasBubbles;
    private bool _isBeingPressed;
    private int _dripChamberCount;

    private void Start()
    {
        _waterLevel.value = 0;
        _raiseAmount = _completionPercent / _holdDuration;
        if (Random.Range(0, 2) < 1) {
            _hasBubbles = true;
            _waterSprite.sprite = _waterLevelBubbleSprite;
        } else {
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
        if (PeriLineControl.Instance.GetCurrentTask() == PeriLineTasks.TAP_CHAMBER)
        {
            _dripChamberCount++;
            if (_dripChamberCount >= 3)
            {
                // okay bubbles gone
                _waterSprite.sprite = _waterLevelSprite;
                PeriLineControl.Instance.MarkCurrentTaskAsDone();
                 _finishTaskEvent.Invoke();
            }
        }
    }

    private void PressBag()
    {
        // Presses the bag..  raise the water level
        if (PeriLineControl.Instance.GetCurrentTask() == PeriLineTasks.SQUEEZE_BAG) {
            if (_waterLevel.value < _maxCompletionPercent) {
                _waterLevel.value += (Time.deltaTime / _holdDuration);
                if (_waterLevel.value > _completionPercent) {
                    // mark as done and move tf on!!
                    PeriLineControl.Instance.MarkCurrentTaskAsDone();

                    if (_hasBubbles) 
                    {
                        PeriLineControl.Instance.AssignTasks(PeriLineTasks.TAP_CHAMBER);
                        _dripChamberCount = 0;
                        _bubbleOccurEvent.Invoke();
                    } 
                    else 
                    {
                        _finishTaskEvent.Invoke();
                    }

                }
            }  
        } else {
            // why they overflowing the bag.. put voice line
        }
    }

    public void StartPress() { _isBeingPressed = true; }
    public void EndPress() { _isBeingPressed = false; }
}

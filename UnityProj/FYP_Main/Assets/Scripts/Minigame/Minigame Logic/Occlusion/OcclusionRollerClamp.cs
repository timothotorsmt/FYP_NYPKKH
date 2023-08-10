using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;

public class OcclusionRollerClamp : TwoWaySlider
{
    
    void Start()
    {

    }

    private void OnEnable()
    {
        switch (OcclusionTaskController.Instance.GetCurrentTask())
        {
            case OcclusionTasks.OPEN_ROLLER_CLAMP:
                _mainSlider.onValueChanged.AddListener(delegate {RollerClampOpenListener();});
                break; 
            default:
                _mainSlider.onValueChanged.AddListener(delegate {WaitForIncorrectInput();});
                break;
        }
    }

    private void OnDisable()
    {
        _mainSlider.onValueChanged.RemoveAllListeners();
    }

    public void SetRollerClampClose()
    {
        _mainSlider.value = 1.0f;
    }

    private void RollerClampOpenListener()
    {
        if (_mainSlider.value <= _sliderOppPassReq && OcclusionTaskController.Instance.GetCurrentTask() == OcclusionTasks.OPEN_ROLLER_CLAMP)
        {
            // Good enough, mark as pass and move on
            OcclusionTaskController.Instance.AssignNextTaskContinuous(OcclusionTasks.START_PUMP);
            OcclusionTaskController.Instance.MarkCurrentTaskAsDone();

            _sliderOppPassEvent.Invoke();
            _mainSlider.onValueChanged.RemoveAllListeners();
            _mainSlider.onValueChanged.AddListener(delegate {WaitForIncorrectInput();});
        }
    }

    private void WaitForIncorrectInput()
    {
        if (_mainSlider.value >= _sliderPassReq)
        {
            // Add fail case 
            OcclusionTaskController.Instance.MarkWrongTask();   
            _sliderOppPassEvent.Invoke();
            _mainSlider.value = _sliderOppPassReq;
            _mainSlider.onValueChanged.RemoveAllListeners();
            _mainSlider.onValueChanged.AddListener(delegate {RollerClampOpenListener();});
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;

public class OcclusionRollerClamp : SliderAction
{
    [SerializeField, Range(0, 1)] private float _reqToClosePass;
    [SerializeField] private UnityEvent _failEvent;
    
    void Start()
    {

    }

    private void OnEnable()
    {
        switch (OcclusionTaskController.Instance.GetCurrentTask())
        {
            case OcclusionTasks.OPEN_ROLLER_CLAMP:
                _slider.onValueChanged.AddListener(delegate {RollerClampOpenListener();});
                break; 
            default:
                _slider.onValueChanged.AddListener(delegate {WaitForIncorrectInput();});
                break;
        }
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveAllListeners();
    }

    public void SetRollerClampClose()
    {
        _slider.value = 1.0f;
    }

    private void RollerClampOpenListener()
    {
        if (_slider.value <= _reqToPass && OcclusionTaskController.Instance.GetCurrentTask() == OcclusionTasks.OPEN_ROLLER_CLAMP)
        {
            // Good enough, mark as pass and move on
            OcclusionTaskController.Instance.AssignNextTaskContinuous(OcclusionTasks.START_PUMP);
            OcclusionTaskController.Instance.MarkCurrentTaskAsDone();
            
            _sliderPassEvent.Invoke();
            _slider.onValueChanged.RemoveAllListeners();
            _slider.onValueChanged.AddListener(delegate {WaitForIncorrectInput();});
        }
    }

    private void WaitForIncorrectInput()
    {
        // Should not be touching it rn
        _slider.value = _reqToPass;
        _failEvent.Invoke();
    }

    
}

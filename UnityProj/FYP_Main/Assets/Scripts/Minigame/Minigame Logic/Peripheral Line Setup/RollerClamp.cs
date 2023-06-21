using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UniRx;

public class RollerClamp : MonoBehaviour
{
    private static float _floatValue;
    [SerializeField] private Slider _rollerClamp;
    [SerializeField] private UnityEvent _clampCloseEvent;
    [SerializeField] private UnityEvent _messUpEvent;

    private void OnDisable()
    {
        _rollerClamp.onValueChanged.RemoveAllListeners();

        _floatValue = _rollerClamp.value;
    }

    private void OnEnable()
    {
        // Assign the appropriate 
        if (PeriLineTaskController.Instance.GetCurrentTask() == PeriLineTasks.CLOSE_CLAMP) {
            //Adds a listener to the main slider and invokes a method when the value changes.
            _rollerClamp.onValueChanged.AddListener (delegate {SetClampToClose();});
        } else if (PeriLineTaskController.Instance.GetCurrentTask() == PeriLineTasks.REMOVE_SPIKE_IV_TUBE || 
            PeriLineTaskController.Instance.GetCurrentTask() == PeriLineTasks.INSERT_SPIKE ||
            PeriLineTaskController.Instance.GetCurrentTask() == PeriLineTasks.SQUEEZE_BAG) 
            {
            _rollerClamp.onValueChanged.AddListener (delegate {WaitforPriming();});
            
        } else if (PeriLineTaskController.Instance.GetCurrentTask() == PeriLineTasks.OPEN_ROLLER_CLAMP)
        {
            _rollerClamp.onValueChanged.AddListener (delegate {SetClampToOpen();});
        }

        _rollerClamp.value = _floatValue;
    }

    // For clamp closing task
    public void SetClampToClose()
    {
        if (_rollerClamp.value >= 0.95f && PeriLineTaskController.Instance.GetCurrentTask() == PeriLineTasks.CLOSE_CLAMP)
        {
            // Good enough, mark as pass and move on
            PeriLineTaskController.Instance.MarkCurrentTaskAsDone();
            _clampCloseEvent.Invoke();
		    _rollerClamp.onValueChanged.RemoveListener (delegate {SetClampToClose();});
		    _rollerClamp.onValueChanged.AddListener (delegate {WaitforPriming();});
        }
    }

    public void SetClampToOpen()
    {
        if (_rollerClamp.value < 0.6f && PeriLineTaskController.Instance.GetCurrentTask() == PeriLineTasks.OPEN_ROLLER_CLAMP)
        {
            // Good enough, mark as pass and move on
            PeriLineTaskController.Instance.MarkCurrentTaskAsDone();
		    _rollerClamp.onValueChanged.RemoveListener (delegate {SetClampToOpen();});
		    _rollerClamp.onValueChanged.AddListener (delegate {WaitforPriming();});
        }
    }

    // for until priming is done
    // For fail state !!
    public void WaitforPriming()
    {
        if (_rollerClamp.value < 0.9f && (PeriLineTaskController.Instance.GetCurrentTask() == PeriLineTasks.REMOVE_SPIKE_IV_TUBE || 
            PeriLineTaskController.Instance.GetCurrentTask() == PeriLineTasks.INSERT_SPIKE ||
            PeriLineTaskController.Instance.GetCurrentTask() == PeriLineTasks.SQUEEZE_BAG)) 
        {
            _messUpEvent.Invoke();
            PeriLineTaskController.Instance.AssignTasks(PeriLineTasks.CLOSE_CLAMP);
		    _rollerClamp.onValueChanged.RemoveListener (delegate {WaitforPriming();});
		    _rollerClamp.onValueChanged.AddListener (delegate {SetClampToClose();});

        }
    }
}

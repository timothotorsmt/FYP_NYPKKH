using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using UniRx;

public class ChlorhexidineWipe : MonoBehaviour
{
    // Add any extra variables
    [SerializeField] private UnityEvent _cleanItemEvent;
    [SerializeField] private GameObject _radialSlider;
    [SerializeField] private DragHover _dragHover;

    private void Start()
    {
        PeripheralSetupTaskController.Instance.CurrentTask.Value.Subscribe(state =>
        {
            if (state == PeripheralSetupTasks.CLEAN_WITH_SWAB)
            {
                _dragHover.SetDisable(false);
            }
            else
            {
                _dragHover.SetDisable(true);
            }
        });
    }

    public void SetSliderComplete()
    {
        if (PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.CLEAN_WITH_SWAB)
        {
            
            // Skip the unpacking of the IV tube right now 
            // TODO: resolve this
            PeripheralSetupTaskController.Instance.AssignNextTaskContinuous(PeripheralSetupTasks.CLAMP_ROLLER_CLAMP);
            PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();
            _cleanItemEvent.Invoke();
            _radialSlider.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using UniRx;

public class AlcoholWipe : MonoBehaviour
{
    // Add any extra variables
    [SerializeField] private UnityEvent _cleanItemEvent;
    [SerializeField] private DragHover _dragHover;

    // Start is called before the first frame update
    void Start()
    {
        PeripheralSetupTaskController.Instance.CurrentTask.Value.Subscribe(state =>
        {
            if (state == PeripheralSetupTasks.CLEAN_IV_CLAVE)
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
        if (PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.CLEAN_IV_CLAVE)
        {
            
            PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();
            _cleanItemEvent.Invoke();
        }
    }
}

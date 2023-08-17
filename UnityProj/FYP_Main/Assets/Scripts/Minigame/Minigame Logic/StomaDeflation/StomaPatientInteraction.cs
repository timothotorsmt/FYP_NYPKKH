using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Events;

public class StomaPatientInteraction : MonoBehaviour
{
    [SerializeField] private DeflationGameLogic _gameLogic;
    [SerializeField] private UnityEvent _backEvent;
    private StomaPatient _currentStomaPatient;
    private float _airbagValue = 0.5f;
    private CompositeDisposable _cd;

    public void SetAirBagValue()
    {
        _gameLogic.GetCurrentPatient().StomaBagAirValue.Value.Subscribe(val =>
        {
      
            _airbagValue = val;
        }).AddTo(_cd);
    }

    public void Back()
    {
        if (DeflationTaskController.Instance.GetCurrentTask() == DeflationTasks.DEFLATE_BAGS)
        {
            _backEvent.Invoke();
        }
        else
        {
            // Patient will complain about you leaving them with their stoma bag open ?? 
            ChatGetter.Instance.StartChat("#DEFLFA");
        }
    }


    // Start is called before the first frame update
    void OnEnable()
    {
        _cd = new CompositeDisposable();
        //SetAirBagValue();
    }

    void OnDisable()
    {
        _cd.Dispose();
    }

    public void Interact()
    {
        if (DeflationTaskController.Instance.GetCurrentTask() == DeflationTasks.DEFLATE_BAGS)
        {
            _gameLogic.GetCurrentPatient().InteractWithPatient();
            if (_gameLogic.GetCurrentPatient().StomaBagAirValue.GetValue() > 0.5)
            {
                DeflationTaskController.Instance.AssignCurrentTaskContinuous(DeflationTasks.UNCLIP_BAG);
            }
            Debug.Log("Whats good i'm at " + _gameLogic.GetCurrentPatient().StomaBagAirValue.GetValue());
        }
    }
}

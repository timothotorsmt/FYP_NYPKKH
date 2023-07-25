using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Events;

public class Bag : MonoBehaviour
{
    [SerializeField] private DeflationGameLogic _gameLogic;
    [SerializeField] private UnityEvent _backEvent;
    [SerializeField] public UnityEvent Dead;
    private StomaPatient _currentStomaPatient;
    private float _airbagValue = 0.5f;
    private float ownAirBagValue = 0;
    private CompositeDisposable _cd;
    GameObject bean;
    public bool Intereced;
    public void SetAirBagValue(float a,GameObject b)
    {
        bean = b;
        ownAirBagValue += a;
    }
    public float GetAirBagValue()
    {
        return ownAirBagValue;
    }
    public void SetAirBagValue(float a)
    {
        ownAirBagValue += a;
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
            if (ownAirBagValue > 0.5)
            {

                ChatGetter.Instance.StartChat("#DEFLIC");
                Intereced = true;
                bean.GetComponent<BEAN>().MoveAway();
                DeflationTaskController.Instance.AssignCurrentTaskContinuous(DeflationTasks.UNCLIP_BAG);
            }

            else
            {
            ChatGetter.Instance.StartChat("#DEFLIB");
            Debug.Log("Whats good i'm at " + ownAirBagValue);

            }
        }
        else if (ownAirBagValue < 0.5)
        {

            ChatGetter.Instance.StartChat("#DEFLIB");
            DeflationTaskController.Instance.AssignCurrentTaskContinuous(DeflationTasks.DEFLATE_BAGS);
        }
    }

    public void beGone()
    {
        _gameLogic.RemoveLife();
        Destroy(gameObject);
    }
}

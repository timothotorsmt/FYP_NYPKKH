using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.Animations;
public class Bag : MonoBehaviour
{
    [SerializeField] private DeflationGameLogic _gameLogic;
    [SerializeField] private UnityEvent _backEvent;
    [SerializeField] public UnityEvent Dead;
    [SerializeField] public UnityEvent PS;
    private StomaPatient _currentStomaPatient;
    public DeflationGameLogic d;
    Animator anim;
    private float _airbagValue = 0.5f;
    private float ownAirBagValue = 0;
    private CompositeDisposable _cd;

    Vector3 ogSize;
    public GameObject bean=null;
    public bool Intereced;
    public bool idle;
    [SerializeField]
    public void SetAirBagValue(float a,GameObject b)
    {
        bean = b;
        ownAirBagValue += a;

      //  Debug.Log("pumping air");
        GetComponent<Animator>().SetTrigger("b");
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

               // ChatGetter.Instance.StartChat("#STOMAC");
                Intereced = true;
                bean.GetComponent<BEAN>().MoveAway();
                bean = null;
                MinigamePerformance.Instance.AddPositiveAction(false);
                transform.localScale =ogSize;
                ownAirBagValue = 0;
                GetComponent<Animator>().SetTrigger("deflate");
            }

            else
            {
            ChatGetter.Instance.StartChat("#STOMAB");
            Debug.Log("Whats good i'm at " + ownAirBagValue);

            }
        }
        else if (ownAirBagValue < 0.5)
        {

            ChatGetter.Instance.StartChat("#STOMAB");
            DeflationTaskController.Instance.AssignCurrentTaskContinuous(DeflationTasks.DEFLATE_BAGS);
        }
    }

    public void beGone()
    {
        if(d.getGamerunning())
        {

            bean = null;
            _gameLogic.RemoveLife();
            PS.Invoke();
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        ogSize = transform.localScale;
        idle = true;
        d = FindObjectOfType<DeflationGameLogic>();
    }

    public void BackToIdle()
    {
        idle = true;
    }
}

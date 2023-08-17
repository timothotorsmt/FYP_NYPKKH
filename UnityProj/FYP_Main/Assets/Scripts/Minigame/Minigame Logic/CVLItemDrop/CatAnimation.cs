using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UIElements;
using TMPro;
//using static UnityEditor.Recorder.OutputPath;
using Core.Input;
using UnityEngine.EventSystems;

public class CatAnimation : MonoBehaviour
{
    float y = 5;
    Tween t;
    public GameObject _bringover;
    GameObject pawPos;
    public List<GameObject> tilt = new List<GameObject>();
    [SerializeField] private UnityEvent _startevent, _BadAlert, _TooMuch, _GoodAlert;
    bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Catappear", Random.Range(3,8));
        pawPos = transform.GetChild(0).gameObject;
       //CatPaw();
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void CatPaw()
    {
        Vector3 targetPosition = new Vector3(-3f, y, -5f); // Set the target position for the wipe animation
        float duration = 4f; // Set the duration of the animation
        
       t= transform.DOMove(targetPosition, duration).SetLoops(-1, LoopType.Yoyo);
        //transform.DORotate(Rotation, 2f, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo);
    }

    

    public void Catappear()
    {
        Vector3 targetPosition = new Vector3(5f, y, -5f); // Set the target position for the wipe animation
        float duration = 4f; // Set the duration of the animation
        Quaternion a = Quaternion.Euler(0, 0, -519.041f);
        Vector3 Rotation = new Vector3(a.eulerAngles.x, a.eulerAngles.y, a.eulerAngles.z);

        transform.DOMove(targetPosition, duration);
        transform.DORotate(Rotation, 2f, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo);

        Invoke("CatPaw",duration);
        _startevent.Invoke();
        InvokeRepeating("CatTouch", 1, 1);
    }

    public void CatTouch()
    {
        Debug.Log("aaa");
        for (int i = 0; i < tilt.Count; i++)
        {
            if(tilt[i]!=null)
            {
                if(Vector2.Distance(pawPos.transform.position, tilt[i].transform.position)<2)
                {
                    Debug.Log("dddd");
                    tilt[i].transform.Rotate(new Vector3(0,0,-25));
                    tilt[i] = null;

                }
            }
        }
    }

    public void BackAway()
    {
        if (t == null)
        {
            CancelInvoke("CatPaw");
        }
        else
        {
            t.Pause();
            t.Complete();
            t.IsComplete();
            t.OnComplete(null);
            t.SetAutoKill(true);
        }
        Vector3 targetPosition =transform.position + new Vector3(0f, 5, 0f); // Set the target position for the wipe animation
        float duration = 4f; // Set the duration of the animation

        transform.DOMove(targetPosition, duration);
        CVLItemTaskController.Instance.MarkCurrentTaskAsDone();

        if(_TooMuch!=null)
        {
            _TooMuch.Invoke();
            done = true;
        }
        else
        {

        }
    }

    public void AlertButton()
    {
        if (done==false)
        {
            
            _BadAlert.Invoke();
        }
        else
        {

            ChatGetter.Instance.StartChat("#CVLCAC", _GoodAlert);
        }
    }

    public void ChangeScene()
    {
        _bringover.SetActive(true);
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Input;
using UnityEngine.Events;
using UnityEngine.UI;
public class WipePS : MonoBehaviour, IInputActions
{
    [SerializeField] private GameObject _destination;
    [SerializeField] private GameObject _dragSign;
    [SerializeField] private UnityEvent _onDropOnDestination;
    [SerializeField] private UnityEvent _onReset;
    public bool _disable;
    private bool _isMoving;
    private bool _isFinished;

    private Vector2 oldPos;

    public Image ps;


    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<Image>();
        Reset();
    }

    public void Reset()
    {
        // Make sure all the shadows r active
        _isFinished = false;
        UIInputManager.Instance.AddSubscriber(this);

        if (_dragSign != null)
        {
            _dragSign.SetActive(true);
        }
        _onReset.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        if (UIInputManager.Instance != null)
        {
            UIInputManager.Instance.AddSubscriber(this);
        }
    }

    void OnDisable()
    {
        if (UIInputManager.Instance != null)
        {
            UIInputManager.Instance.RemoveSubscriber(this);
        }
    }

    public void SetDisable(bool newState)
    {
        if (newState)
        {
            this.gameObject.SetActive(false);
            _disable = true;
        }
        else
        {
            this.gameObject.SetActive(true);
            _disable = false;
        }
    }

    public void OnStartTap()
    {
        // dc if its finished
        if (_isFinished) { return; }

        Vector2 mousePos = InputUtils.GetInputPosition();
        oldPos = mousePos;
        //if (Vector2.Distance((Vector2)this.transform.position, mousePos) < 1.0f)
        //{
        //    _startPosition = ((Vector2)this.transform.position - mousePos);
        //    _isMoving = true;

        //    if (_dragSign != null)
        //    {
        //        _dragSign.SetActive(false);
        //    }
        //}
    }

    public void OnTap()
    {
        
        Vector2 mousePos = InputUtils.GetInputPosition();
        if(mousePos !=oldPos && ps.enabled)
        {
      
            Color tempColor = ps.color;
            tempColor.a -= 0.4f*Time.deltaTime;
            ps.color = tempColor;
            oldPos = mousePos;
            Debug.Log($"{ ps.color.a}");
            if(ps.color.a <=.2)
            {
                ps.enabled = false;
                tempColor = ps.color;
                tempColor.a = 1;
                ps.color = tempColor;
            }
        }
        
    }

    public void OnEndTap()
    {
      
    }
}

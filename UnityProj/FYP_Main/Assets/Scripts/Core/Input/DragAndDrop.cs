using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Input;
using UnityEngine.Events;

public class DragAndDrop : MonoBehaviour, IInputActions
{
    [SerializeField] private GameObject _destination;
    [SerializeField] private GameObject _dragSign;
    [SerializeField] private UnityEvent _onDropOnDestination;
    [SerializeField] private UnityEvent _onReset;
    public bool _disable;
    private bool _isMoving;
    private bool _isFinished;

    private Vector2 _startPosition;
    private Vector2 _resetPosition;
    private RectTransform _rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _resetPosition = _rectTransform.anchoredPosition;
        Reset();
    }

    public void Reset()
    {
        // Make sure all the shadows r active
        _isFinished = false;
        UIInputManager.Instance.AddSubscriber(this);
        _rectTransform.anchoredPosition = _resetPosition;

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

        if (Vector2.Distance((Vector2)this.transform.position, mousePos) < 1.0f)
        {
            _startPosition = ((Vector2)this.transform.position - mousePos);
            _isMoving = true;

            if (_dragSign != null)
            {
                _dragSign.SetActive(false);
            }
        }
    }

    public void OnTap()
    {   
        if (_isMoving)
        {
            Vector2 mousePos = InputUtils.GetInputPosition();

            this.transform.position = mousePos + _startPosition;
        }
    }

    public void OnEndTap()
    {
        _isMoving = false;

        if (Vector2.Distance((Vector2)this.transform.position, (Vector2)_destination.transform.position) < 1.0f)
        {
            this.transform.position = new Vector3(_destination.transform.position.x, _destination.transform.position.y, _destination.transform.position.z);
            _isFinished = true;
            _onDropOnDestination.Invoke();
        }
        else
        {
            if (_rectTransform) {
                _rectTransform.anchoredPosition = _resetPosition;
            }
            if (_dragSign != null)
            {
                _dragSign.SetActive(true);
            }
        }
    }
}

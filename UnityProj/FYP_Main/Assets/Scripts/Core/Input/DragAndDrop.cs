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
    private bool _isMoving;
    private bool _isFinished;

    private Vector2 _startPosition;
    private Vector3 _resetPosition;

    // Start is called before the first frame update
    void Start()
    {
        _resetPosition = this.transform.localPosition;
        Reset();
    }

    public void Reset()
    {
        // Make sure all the shadows r active
        _isFinished = false;
        UIInputManager.Instance.AddSubscriber(this);
        this.transform.localPosition = _resetPosition;

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
                Debug.Log("Wyh");
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
            _onDropOnDestination.Invoke();
            _isFinished = true;
        }
        else
        {
            this.transform.localPosition = new Vector3(_resetPosition.x, _resetPosition.y, _resetPosition.z);
            if (_dragSign != null)
            {
                _dragSign.SetActive(true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Input;
using UnityEngine.Events;

public class DragAndDrop : MonoBehaviour, IInputActions
{
    #region Drag and Drop Variables
    [Tooltip("The destination of the gameobject")] [SerializeField] private GameObject _destination;
    [Tooltip("An indicator for the dragging (if any)")] [SerializeField] private GameObject _indicator;
    [SerializeField] private GameObject _dragSign;
    [SerializeField] private UnityEvent _onDropOnDestination;
    [SerializeField] private UnityEvent _onReset;
    [SerializeField, Range(0, 10)] private float _dragDistance = 1.0f;
    public bool _disable;
    private bool _isMoving;
    private bool _isFinished;

    private Vector2 _startPosition;
    private Vector2 _resetPosition;
    private RectTransform _rectTransform;
    #endregion

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
        // Mainly is for UI elements, so i use this class
        UIInputManager.Instance.AddSubscriber(this);
        _rectTransform.anchoredPosition = _resetPosition;

        if (_dragSign != null)
        {
            _dragSign.SetActive(true);
        }
        if (_indicator != null)
        {
            _indicator.SetActive(false);
        }

        _onReset.Invoke();
    }

    
    void OnEnable()
    {
        if (UIInputManager.Instance != null)
        {
            // Add to UI input manager
            UIInputManager.Instance.AddSubscriber(this);
        }
    }

    void OnDisable()
    {
        if (UIInputManager.Instance != null)
        {
            // Remove UI input manager
            UIInputManager.Instance.RemoveSubscriber(this);
        }
    }

    public void SetDisable(bool newState, bool affectGameObject = true)
    {
        if (newState)
        {
            if (affectGameObject)
            {
                this.gameObject.SetActive(false);
            }

            // Disable the drag sign and the indicator
            if (_dragSign != null)
            {
                _dragSign.SetActive(false);
            }
            if (_indicator != null)
            {
                _indicator.SetActive(false);
            }

            _disable = true;
        }
        else 
        {
            if (affectGameObject)
            {
                this.gameObject.SetActive(true);
            }

            // Return the drag sign back because !! 
            if (_dragSign != null)
            {
                _dragSign.SetActive(true);
            }
            _disable = false;
        }
    }

    public void OnStartTap()
    {
        // dc if its finished
        if (_isFinished) { return; }

        Vector2 mousePos = InputUtils.GetInputPosition();

        if (Vector2.Distance((Vector2)this.transform.position, mousePos) < _dragDistance)
        {
            _startPosition = ((Vector2)this.transform.position - mousePos);
            _isMoving = true;

            if (_dragSign != null)
            {
                _dragSign.SetActive(false);
            }
            if (_indicator != null)
            {
                _indicator.SetActive(true);
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
            if (_indicator != null)
            {
                _indicator.SetActive(false);
            }

        }
    }
}

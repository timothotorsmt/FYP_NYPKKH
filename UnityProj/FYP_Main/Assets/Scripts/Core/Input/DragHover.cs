using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Input;
using UnityEngine.Events;
using UnityEngine.UI;
using UniRx;
using Unity.VisualScripting;

public class DragHover : MonoBehaviour, IInputActions
{
    [SerializeField] private GameObject _destination;
    [SerializeField, Min(0)] private float _hoverDuration = 3.0f;
    [SerializeField, Min(0)] private float _hoverForgivance = 1.0f;
    [SerializeField] private Image _hoverSlider;
    [SerializeField] private GameObject _sliderObject;
    [SerializeField] private GameObject _dragSign;
    [SerializeField] private UnityEvent _onDropOnDestination;
    [SerializeField] private UnityEvent _onReset;
    public bool _disable;
    private bool _isMoving;
    private bool _isFinished;

    private float _hoverTimer;

    private Vector2 _startPosition;
    private Vector2 _prevPosition;
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
        _sliderObject.SetActive(false);
        _rectTransform.anchoredPosition = _resetPosition;
        _hoverTimer = 0;

        if (_dragSign != null)
        {
            _dragSign.SetActive(true);
        }
        _onReset.Invoke();

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
            _hoverSlider.gameObject.SetActive(false);
            _sliderObject.SetActive(false);
            _disable = true;
        }
        else 
        {
            _hoverSlider.gameObject.SetActive(true);
            _sliderObject.SetActive(true);
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

        _prevPosition = _startPosition;

    }

    public void OnTap()
    {   
        if (_isMoving)
        {
            Vector2 mousePos = (Vector2)InputUtils.GetInputPosition() + _startPosition;

            if ((mousePos - _prevPosition).SqrMagnitude() > Mathf.Epsilon && !_disable)
            {
                if (Vector2.Distance((Vector2)mousePos, (Vector2)_destination.transform.position) < _hoverForgivance)
                {
                    _sliderObject.SetActive(true);

                    // if its moving
                    _hoverTimer += Time.deltaTime;
                    _hoverSlider.fillAmount = _hoverTimer / _hoverDuration;
                    if (_hoverTimer >= _hoverDuration)
                    {
                        _onDropOnDestination.Invoke();
                        _isFinished = true;
                    }
                }

            }

            _prevPosition = mousePos;

            this.transform.position = mousePos;
        }
    }

    public void OnEndTap()
    {
        _isMoving = false;
        _sliderObject.SetActive(false);

        if (_rectTransform) {
            _rectTransform.anchoredPosition = _resetPosition;
        }
        if (_dragSign != null)
        {
            _dragSign.SetActive(true);
        }
    }
}

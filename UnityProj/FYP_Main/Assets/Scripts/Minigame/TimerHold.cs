using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UniRx.Extention;

public class TimerHold : BasicSlider
{
    // Timer hold variables
    private bool _isBeingPressed;

    [SerializeField] private BoxCollider2D _indicator; // The indicator arrow
    [SerializeField] private GameObject _pressIndicator; // The indicator arrow
    [SerializeField] private BoxCollider2D _idealZone; // The box with the ideal win zone
    [SerializeField] private float _holdDuration = 5.0f; // How long you need to hold before the thing does crazyyy
    [SerializeField] private UnityEvent _failCaseLate; // What happens if you go over
    [SerializeField] private UnityEvent _failCaseEarly; // What happens if you go over
    [SerializeField] private bool _isReversible = false; // Does it go backwards if its let go\

    public UnityEvent OnAcceptableRange;
    public UnityEvent OnLateRange;
    public bool Interactable = true;
    public ReactiveProp<float> IndicatorValue = new ReactiveProp<float>();

    // Start is called before the first frame update
    void Start()
    {
        _mainSlider.value = 0;
        _indicator.size = new Vector2(_indicator.gameObject.GetComponent<RectTransform>().rect.width, _indicator.gameObject.GetComponent<RectTransform>().rect.height);
        _idealZone.size = new Vector2(_idealZone.gameObject.GetComponent<RectTransform>().rect.width, _idealZone.gameObject.GetComponent<RectTransform>().rect.height);
        _idealZone.offset = Vector2.zero;

    }

    private void OnEnable()
    {
        _isBeingPressed = false;

        _mainSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Interactable) 
        {
            if (_isBeingPressed) { AddTimerValue(); }
            else if (_isReversible) { Reduce(); }
        }
    }

    private void AddTimerValue()
    {
        _mainSlider.value += (Time.deltaTime / _holdDuration);

        // Just for this one minigame
        // Does nothing if no subscribers
        if (_indicator.bounds.center.x <= _idealZone.bounds.max.x && _indicator.bounds.center.x >= _idealZone.bounds.min.x)
        {
            // Win
            OnAcceptableRange.Invoke();
        }
        else if (_indicator.bounds.center.x > _idealZone.bounds.max.x)
        {
            OnLateRange.Invoke();
        }

        if (_mainSlider.value > _mainSlider.maxValue * 0.95f)
        {
            // Stop 
            _failCaseLate.Invoke();
            _mainSlider.value = 0;
        }

        IndicatorValue.SetValue(_mainSlider.value);
    }

    private void Reduce()
    {
        if (_mainSlider.value > 0)
        {
            _mainSlider.value -= (Time.deltaTime / (_holdDuration * 2));
        }

        IndicatorValue.SetValue(_mainSlider.value);
    }

    public void StartPress() 
    { 
        _isBeingPressed = true; 
    }

    public void EndPress() 
    { 
        _isBeingPressed = false;

        // Check if passed the check
        if (_indicator.bounds.center.x <= _idealZone.bounds.max.x && _indicator.bounds.center.x >= _idealZone.bounds.min.x)
        {
            // Win
            _sliderPassEvent.Invoke();
            _pressIndicator.SetActive(false);
        }
        else if (_indicator.bounds.center.x > _idealZone.bounds.max.x && !_isReversible)
        {
            _failCaseLate.Invoke();
            _mainSlider.value = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TimerHold : OneWaySlider
{
    // Timer hold variables
    private bool _isBeingPressed;

    [SerializeField] private BoxCollider2D _indicator; // The indicator arrow
    [SerializeField] private BoxCollider2D _idealZone; // The box with the ideal win zone
    [SerializeField] private float _holdDuration = 5.0f; // How long you need to hold before the thing does crazyyy
    [SerializeField] private UnityEvent _failCaseLate; // What happens if you go over
    [SerializeField] private UnityEvent _failCaseEarly; // What happens if you go over
    [SerializeField] private bool _isReversible = false; // Does it go backwards if its let go

    // Start is called before the first frame update
    void Start()
    {
        _mainSlider.value = 0;
        _indicator.size = new Vector2(_indicator.gameObject.GetComponent<RectTransform>().rect.width, _indicator.gameObject.GetComponent<RectTransform>().rect.height);
        _idealZone.size = new Vector2(_idealZone.gameObject.GetComponent<RectTransform>().rect.width, _idealZone.gameObject.GetComponent<RectTransform>().rect.height);
        _idealZone.offset = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isBeingPressed) { AddTimerValue(); }
        else if (_isReversible) { Reduce(); }
    }

    private void AddTimerValue()
    {
        _mainSlider.value += (Time.deltaTime / _holdDuration);
        if (_mainSlider.value > _mainSlider.maxValue * 0.95f)
        {
            // Stop 
            _failCaseLate.Invoke();
            _mainSlider.value = 0;
        }
    }

    private void Reduce()
    {
        if (_mainSlider.value < _mainSlider.maxValue * 0.01f)
        {
            _mainSlider.value -= (Time.deltaTime / _holdDuration);
        }

    }

    public void StartPress() { _isBeingPressed = true; }
    public void EndPress() 
    { 
        _isBeingPressed = false;

        // Check if passed the check
        if (_indicator.bounds.center.x <= _idealZone.bounds.max.x && _indicator.bounds.center.x >= _idealZone.bounds.min.x)
        {
            // Win
            _sliderPassEvent.Invoke();
        }
        else if (_indicator.bounds.center.x > _idealZone.bounds.max.x)
        {
            _failCaseLate.Invoke();
            _mainSlider.value = 0;
        }
    }
}

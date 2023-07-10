using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TimerHold : OneWaySlider
{
    // Timer hold variables
    private bool _isBeingPressed;

    [SerializeField] private BoxCollider2D _indicator;
    [SerializeField] private BoxCollider2D _idealZone;
    [SerializeField] private float _holdDuration = 5.0f;
    [SerializeField] private UnityEvent _failCase;

    // Start is called before the first frame update
    void Start()
    {
        _mainSlider.value = 0;
        _indicator.size = new Vector2(_indicator.gameObject.GetComponent<RectTransform>().rect.width, _indicator.gameObject.GetComponent<RectTransform>().rect.height);
        _idealZone.size = new Vector2(_idealZone.gameObject.GetComponent<RectTransform>().rect.width, _idealZone.gameObject.GetComponent<RectTransform>().rect.height);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isBeingPressed) { AddTimerValue(); }
    }

    private void AddTimerValue()
    {
        _mainSlider.value += (Time.deltaTime / _holdDuration);
        if (_mainSlider.value > _mainSlider.maxValue * 0.95f)
        {
            // Stop 
            _failCase.Invoke();
            _mainSlider.value = 0;
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
            _failCase.Invoke();
            _mainSlider.value = 0;
        }
    }
}

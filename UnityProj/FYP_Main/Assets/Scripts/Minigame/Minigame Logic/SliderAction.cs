using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UniRx.Extention;
using UniRx;
using UnityEngine.EventSystems;
using DG.Tweening;

// This was the crusty ass class I used before (it sucks)
public class SliderAction : MonoBehaviour
{
    [SerializeField, Range(0,1)] protected float _reqToPass = 0.5f;
    [SerializeField] protected Slider _slider;
    [SerializeField] protected UnityEvent _sliderPassEvent;
}

public class BasicSlider : MonoBehaviour
{
    [SerializeField] protected Slider _mainSlider;
    [SerializeField, Range(0, 1)] protected float _sliderPassReq = 0.95f;
    [SerializeField] protected UnityEvent _sliderPassEvent;

    protected bool isIdealState = false;

    // Don't actually know if will use LOL just try only
    protected ReactiveProp<float> _sliderValue;

    private void Awake()
    {
        _sliderValue = new ReactiveProp<float>();
    }

    protected void DisableSlider(bool fade = false)
    {
        _mainSlider.interactable = false;
        if (fade)
        {
            if (_mainSlider.gameObject.GetComponent<CanvasGroup>() != null)
            {
                _mainSlider.gameObject.GetComponent<CanvasGroup>().DOFade(0, 1.0f);
            }
            else
            {
                Debug.LogWarning("Canvas group component is missing. Did you forget to add a canvas group to the slider gameObject?");
            }
        }
    }
}

public class TwoWaySlider : BasicSlider
{
    [SerializeField, Range(0, 1)] protected float _sliderOppPassReq = 0.05f;

    [SerializeField] protected UnityEvent _sliderOppPassEvent;
}

public class OneWaySlider : BasicSlider
{
    [Tooltip("if _isForward is true, that prevents backwards movement (i.e. going back to 0), otherwise it prevents forwards movement (i.e. going up to 1)")]
    [SerializeField] protected bool _isForward = true;
    private float _sliderPrevValue; // Tracks the last known value

    private void Awake()
    {
        _sliderPrevValue = _mainSlider.value;
        _mainSlider.onValueChanged.AddListener(delegate { PreventBackflow(); });
    }

    private void PreventBackflow()
    {
        if (_isForward)
        {
            if (_mainSlider.value < _sliderPrevValue)
            {
                // illegal movement, set back
                _mainSlider.value = _sliderPrevValue;
            }
        }
        else
        {
            if (_mainSlider.value > _sliderPrevValue)
            {
                // illegal movement, set forward
                _mainSlider.value = _sliderPrevValue;
            }
        }
    }
}
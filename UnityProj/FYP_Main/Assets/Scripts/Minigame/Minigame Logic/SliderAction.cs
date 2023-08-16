using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UniRx.Extention;
using UniRx;
using UnityEngine.EventSystems;
using DG.Tweening;

// Like slider, but for multiple sliders at the same time
public class MultiSlider : MonoBehaviour
{
    [SerializeField] protected List<Slider> _sliders;
    [SerializeField, Range(0, 1)] protected float _sliderPassReq;
    [SerializeField] protected UnityEvent _sliderPassEvent;   
}

// The simplest version of the slider behaviour class
public class BasicSlider : MonoBehaviour
{
    [SerializeField] protected Slider _mainSlider;
    [SerializeField, Range(0, 1)] protected float _sliderPassReq;
    [SerializeField] protected UnityEvent _sliderPassEvent;

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

        _sliderPrevValue = _mainSlider.value;
    }
}
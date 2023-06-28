using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Default variables for all sliders 
public class SliderAction : MonoBehaviour
{
    [SerializeField, Range(0,1)] protected float _reqToPass = 0.5f;
    [SerializeField] protected Slider _slider;
    [SerializeField] protected UnityEvent _sliderPassEvent;
}

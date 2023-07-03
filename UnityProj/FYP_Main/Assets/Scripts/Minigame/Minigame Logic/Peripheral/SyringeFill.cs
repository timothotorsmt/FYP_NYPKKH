using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SyringeFill : SliderAction
{
    [SerializeField, Min(0)] private float _pressDuration = 3.0f;
    private float _pressTimer;
    private bool _isPress;


    // Start is called before the first frame update
    void Update()
    {
        if (_isPress)
        {
            OnPress();
        }   
    }

    public void OnPress()
    {
        if (PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.FLUSH_IV_PLUG_CLAVE)
        {
            // if its moving
            _pressTimer += Time.deltaTime;
            _slider.value = _pressTimer / _pressDuration;
            if (_slider.value >= _reqToPass)
            {
                PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();
                _sliderPassEvent.Invoke();
            }
        }
    }

    public void PressDown()
    {
        _isPress = true;
    }

    public void PressUp()
    {
        _isPress = false;
    }
}

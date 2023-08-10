using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Extention;
using System.Linq;
using UnityEngine.UI;

public class UnclipBag : MultiSlider
{
    void OnEnable()
    {
        foreach (Slider slider in _sliders)
        {
            slider.onValueChanged.AddListener(delegate { CheckIfAllDone(); });
        }   
    }

    void OnDisable()
    {
        foreach (Slider slider in _sliders)
        {
            slider.onValueChanged.RemoveAllListeners();
        }
    }

    // Check
    void CheckIfAllDone()
    {
        if (_sliders.Select(slider => slider.value).Where(x => x >= _sliderPassReq).Count() == _sliders.Count())
        {
            // yes all done!
            if (DeflationProcedureTaskController.Instance.GetCurrentTask() == DeflationProcedureTasks.UNCLIP_BAG)
            {
                // Mark current task as done!! Move on
                DeflationProcedureTaskController.Instance.MarkCurrentTaskAsDone();
                _sliderPassEvent.Invoke();
            }
        }
    }

    public void Reset()
    {
        foreach (Slider slider in _sliders)
        {
            slider.value = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Extention;
using System.Linq;
using UnityEngine.UI;

public class ClipBag : MultiSlider
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
            if (DeflationTaskController.Instance.GetCurrentTask() == DeflationTasks.CLIP_BAG)
            {
                // Mark current task as done!! Move on
                DeflationTaskController.Instance.MarkCurrentTaskAsDone();
                _sliderPassEvent.Invoke();
            }
        }
    }
}

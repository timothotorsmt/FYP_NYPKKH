using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

public class PullOutIV : BasicSlider
{
    [SerializeField] private Slider _pullOutIV;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDisable()
    {
        _pullOutIV.onValueChanged.RemoveAllListeners();
    }


    private void OnEnable()
    {
        _pullOutIV.onValueChanged.AddListener(delegate { RemoveIVItem(); } );
    }

    private void RemoveIVItem()
    {
        _mainSlider.value = _pullOutIV.value;
   
        if (_mainSlider.value >= _sliderPassReq && OcclusionTaskController.Instance.GetCurrentTask() == OcclusionTasks.PULL_OUT_PLUG)
        {
            // Good enough, mark as pass and move on
            OcclusionTaskController.Instance.MarkCurrentTaskAsDone();
            _sliderPassEvent.Invoke();
            _pullOutIV.interactable = false;
            _pullOutIV.onValueChanged.RemoveListener(delegate { RemoveIVItem(); });
        }

    }
}

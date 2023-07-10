using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

public class RemoveTubing : BasicSlider
{
    [SerializeField] private Slider _removeIVTube;
    [SerializeField] private Image _tubing;

    private void OnDisable()
    {
        _removeIVTube.onValueChanged.RemoveAllListeners();
    }

    private void OnEnable()
    {
        if (OcclusionTaskController.Instance.GetCurrentTask() == OcclusionTasks.DISCONNECT_TUBING_FROM_PLUG)
        {
            _removeIVTube.onValueChanged.AddListener(delegate { SetRemoveIVTube(); });
        }
        
    }

    private void SetRemoveIVTube()
    {
        _mainSlider.value = _removeIVTube.value;
        
        if (_mainSlider.value >= _sliderPassReq && OcclusionTaskController.Instance.GetCurrentTask() == OcclusionTasks.DISCONNECT_TUBING_FROM_PLUG)
        {
            OcclusionTaskController.Instance.MarkCurrentTaskAsDone();
            _sliderPassEvent.Invoke();
            _removeIVTube.interactable = false;
            _tubing.GetComponent<Image>().DOFade(0, 1.0f);
        }
    }
}

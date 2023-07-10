using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

public class RemoveTegaderm : BasicSlider
{
    [SerializeField] private Image _tegaderm;

    // Start is called before the first frame update
    void Start()
    {
        OcclusionTaskController.Instance.CurrentTask.Value.Subscribe(state => 
        {
            if (state == OcclusionTasks.REMOVE_TEGADERM)
            {
                _mainSlider.onValueChanged.AddListener(delegate { SetRemoveIVTube(); });
            }
        });
    }

    private void SetRemoveIVTube()
    {        
        if (_mainSlider.value >= _sliderPassReq && OcclusionTaskController.Instance.GetCurrentTask() == OcclusionTasks.REMOVE_TEGADERM)
        {
            OcclusionTaskController.Instance.MarkCurrentTaskAsDone();
            _sliderPassEvent.Invoke();
            _mainSlider.interactable = false;
            _mainSlider.gameObject.SetActive(false);
            _tegaderm.DOFade(0, 1.0f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

public class RemoveTegaderm : SliderAction
{
    [SerializeField] private Image _tegaderm;

    // Start is called before the first frame update
    void Start()
    {
        OcclusionTaskController.Instance.CurrentTask.Value.Subscribe(state => 
        {
            if (state == OcclusionTasks.REMOVE_TEGADERM)
            {
                _slider.onValueChanged.AddListener(delegate { SetRemoveIVTube(); });
            }
        });
    }

    private void SetRemoveIVTube()
    {        
        if (_slider.value >= _reqToPass && OcclusionTaskController.Instance.GetCurrentTask() == OcclusionTasks.REMOVE_TEGADERM)
        {
            OcclusionTaskController.Instance.MarkCurrentTaskAsDone();
            _sliderPassEvent.Invoke();
            _slider.interactable = false;
            _slider.gameObject.SetActive(false);
            _tegaderm.DOFade(0, 1.0f);
        }
    }
}

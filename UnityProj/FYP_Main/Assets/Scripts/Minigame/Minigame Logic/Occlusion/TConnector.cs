using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TConnector : SliderAction
{
    [SerializeField] private Slider _tConnectorOpen;
    [SerializeField] private Slider _tConnectorUnclamp;

    private void OnDisable()
    {
        _tConnectorOpen.onValueChanged.RemoveAllListeners();
        _tConnectorUnclamp.onValueChanged.RemoveAllListeners();
    }

    private void OnEnable()
    {
        _tConnectorUnclamp.onValueChanged.AddListener(delegate { SetUnClampItem(); });
        _tConnectorOpen.onValueChanged.AddListener(delegate { SetClampItem(); });
    }

    private void SetClampItem()
    {
        _slider.value = _tConnectorOpen.value;
   
        if (_slider.value >= _reqToPass && OcclusionTaskController.Instance.GetCurrentTask() == OcclusionTasks.CLAMP_T_CONNECTOR)
        {
            // Good enough, mark as pass and move on
            OcclusionTaskController.Instance.MarkCurrentTaskAsDone();
            _sliderPassEvent.Invoke();
            _tConnectorOpen.interactable = false;
            _tConnectorOpen.onValueChanged.RemoveListener(delegate { SetClampItem(); });
        }

    }

    private void SetUnClampItem()
    {
        _slider.value = 1 - _tConnectorUnclamp.value;

        if (_tConnectorUnclamp.value >= _reqToPass && OcclusionTaskController.Instance.GetCurrentTask() == OcclusionTasks.UNCLAMP_T_CONNECTOR)
        {
            // Good enough, mark as pass and move on
            OcclusionTaskController.Instance.MarkCurrentTaskAsDone();
            _sliderPassEvent.Invoke();
            _tConnectorUnclamp.interactable = false;
            _tConnectorUnclamp.onValueChanged.RemoveListener(delegate { SetUnClampItem(); });
        }

    }

}

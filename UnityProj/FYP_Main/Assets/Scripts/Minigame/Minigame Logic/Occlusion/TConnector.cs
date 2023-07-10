using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

public class TConnector : BasicSlider
{
    [SerializeField] private Slider _tConnectorOpen;
    [SerializeField] private Slider _tConnectorUnclamp;
    [SerializeField] private GameObject _tConnectorGameObject;

    private CompositeDisposable _cd;

    private void OnDisable()
    {
        _cd.Dispose();
        _tConnectorOpen.onValueChanged.RemoveAllListeners();
        _tConnectorUnclamp.onValueChanged.RemoveAllListeners();
    }

    private void OnEnable()
    {
        _cd = new CompositeDisposable();
        OcclusionTaskController.Instance.CurrentTask.Value.Subscribe(state => 
        {
            if (state == OcclusionTasks.UNCLAMP_T_CONNECTOR)
            {
                _tConnectorUnclamp.onValueChanged.AddListener(delegate { SetUnClampItem(); });
                _tConnectorUnclamp.gameObject.SetActive(true);
                _tConnectorOpen.gameObject.SetActive(false);
                _tConnectorGameObject.SetActive(true);
            }
            else if (state == OcclusionTasks.CLAMP_T_CONNECTOR)
            {
                _tConnectorOpen.onValueChanged.AddListener(delegate { SetClampItem(); });
                _tConnectorUnclamp.gameObject.SetActive(false);
                _tConnectorOpen.gameObject.SetActive(true);
                _tConnectorGameObject.SetActive(true);
            }
            else 
            {
                _tConnectorUnclamp.gameObject.SetActive(false);
                _tConnectorOpen.gameObject.SetActive(false);
                _tConnectorGameObject.SetActive(false);
            }
        }).AddTo(_cd);
    }

    private void SetClampItem()
    {
        _mainSlider.value = _tConnectorOpen.value;
   
        if (_mainSlider.value >= _sliderPassReq && OcclusionTaskController.Instance.GetCurrentTask() == OcclusionTasks.CLAMP_T_CONNECTOR)
        {
            // Good enough, mark as pass and move on
            OcclusionTaskController.Instance.MarkCurrentTaskAsDone();
            _sliderPassEvent.Invoke();
            _tConnectorOpen.interactable = false;
            _tConnectorOpen.onValueChanged.RemoveListener(delegate { SetClampItem(); });
        }

    }

    public void CloseTConnector()
    {
        _mainSlider.value = 1;
    }

    private void SetUnClampItem()
    {
        _mainSlider.value = 1 - _tConnectorUnclamp.value;

        if (_tConnectorUnclamp.value >= _sliderPassReq && OcclusionTaskController.Instance.GetCurrentTask() == OcclusionTasks.UNCLAMP_T_CONNECTOR)
        {
            // Good enough, mark as pass and move on
            OcclusionTaskController.Instance.AssignNextTaskContinuous(OcclusionTasks.START_PUMP);
            OcclusionTaskController.Instance.MarkCurrentTaskAsDone();
            _sliderPassEvent.Invoke();
            _tConnectorUnclamp.interactable = false;
            _tConnectorUnclamp.onValueChanged.RemoveListener(delegate { SetUnClampItem(); });
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SpikeCap : MonoBehaviour
{
    // Boolean to mark as false
    private bool _isRunning;

    [SerializeField] private Slider _spikeCap;
    [SerializeField] private Image _spikeHandle;
    [SerializeField] private UnityEvent _capCloseEvent;
    [SerializeField, Range(0, 1)] private float _capClosePercentage = 0.7f;

    private void OnDisable()
    {
        _spikeCap.onValueChanged.RemoveAllListeners();

    }

    private void OnEnable()
    {
        _spikeCap.onValueChanged.AddListener(delegate { removeSpikeCap(); });
        _isRunning = false;
    }

    // for closing the clamp
    public void removeSpikeCap()
    {
        if (_spikeCap.value >= _capClosePercentage && PeriLineTaskController.Instance.GetCurrentTask() == PeriLineTasks.REMOVE_SPIKE_IV_TUBE && !_isRunning)
        {
            // Good enough, mark as pass and move on
            _capCloseEvent.Invoke();
            Sequence seq = DOTween.Sequence();
            _isRunning = true;
            // function is not needed anymore bye!
            _spikeCap.onValueChanged.RemoveListener(delegate { removeSpikeCap(); });

            seq.Append(_spikeHandle.DOFade(0, 1.0f));
            seq.AppendCallback(() => PeriLineTaskController.Instance.MarkCurrentTaskAsDone());
            seq.AppendCallback(() => _spikeCap.gameObject.SetActive(false));
            // theoretically this does not matter. but. we will set _isrunning to false anyway.
            seq.AppendCallback(() => _isRunning = false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SpikeInsertion : MonoBehaviour
{
    [SerializeField] private Slider _spike;
    [SerializeField] private UnityEvent _capCloseEvent;
    [SerializeField, Range(0, 1)] private float _capClosePercentage = 0.95f;

    private void OnDisable()
    {
        _spike.onValueChanged.RemoveAllListeners();
    }

    private void OnEnable()
    {
        _spike.onValueChanged.AddListener(delegate { InsertSpikeIntoBag(); });
    }

    // for closing the clamp
    public void InsertSpikeIntoBag()
    {
        if (_spike.value >= _capClosePercentage && PeriLineTaskController.Instance.GetCurrentTask() == PeriLineTasks.INSERT_SPIKE)
        {
            // Good enough, mark as pass and move on
            _capCloseEvent.Invoke();
            Sequence seq = DOTween.Sequence();
            // function is not needed anymore bye!
            _spike.onValueChanged.RemoveListener(delegate { InsertSpikeIntoBag(); });

            PeriLineTaskController.Instance.MarkCurrentTaskAsDone();
        }
    }
}

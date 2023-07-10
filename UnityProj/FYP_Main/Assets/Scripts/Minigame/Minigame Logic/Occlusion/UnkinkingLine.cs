using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UnkinkingLine : BasicSlider
{
    // Add any extra variables
    [SerializeField] private GameObject _kinkedLine;
    [SerializeField] private GameObject _unkinedLine;

    private void OnDisable()
    {
        _mainSlider.onValueChanged.RemoveAllListeners();
        DOTween.Kill(this);
    }

    private void OnEnable()
    {
        if (OcclusionTaskController.Instance.GetCurrentTask() == OcclusionTasks.UNKINK_LINE)
        {
            _mainSlider.onValueChanged.AddListener(delegate { SetSliderComplete(); });
            _kinkedLine.SetActive(true);
            _unkinedLine.SetActive(false);
        }
        else
        {
            _kinkedLine.SetActive(false);
            _unkinedLine.SetActive(true);
            _mainSlider.gameObject.SetActive(false);
        }
    }

    private void SetSliderComplete()
    {
        if (_mainSlider.value >= _sliderPassReq && OcclusionTaskController.Instance.GetCurrentTask() == OcclusionTasks.UNKINK_LINE)
        {
            // Good enough, mark as pass and move on
            OcclusionTaskController.Instance.AssignNextTaskContinuous(OcclusionTasks.START_PUMP);
            OcclusionTaskController.Instance.MarkCurrentTaskAsDone();
            _sliderPassEvent.Invoke();
            _mainSlider.interactable = false;

            // Animation Sequence
            Sequence seq = DOTween.Sequence();
            _unkinedLine.SetActive(true);
            seq.PrependInterval(0.5f);
            seq.Append(_kinkedLine.GetComponent<Image>().DOFade(0, 0.5f)).SetEase(Ease.Linear);
            seq.Join(_unkinedLine.GetComponent<Image>().DOFade(1, 0.5f)).SetEase(Ease.Linear);

            _mainSlider.onValueChanged.RemoveListener(delegate { SetSliderComplete(); });
        }


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnrollBag : BasicSlider
{
    private void OnDisable()
    {
        _mainSlider.onValueChanged.RemoveAllListeners();
    }

    private void OnEnable()
    {
        _mainSlider.onValueChanged.AddListener(delegate { SetSliderComplete(); });
    }

    private void SetSliderComplete()
    {
        if (_mainSlider.value >= _sliderPassReq && DeflationProcedureTaskController.Instance.GetCurrentTask() == DeflationProcedureTasks.UNROLL_BAG)
        {
            // Good enough, mark as pass and move on
            DeflationProcedureTaskController.Instance.MarkCurrentTaskAsDone();
            _sliderPassEvent.Invoke();

            if (MinigameManager.Instance.GetMinigameDifficulty().GameDifficulty != Difficulty.BOSS)
            {
                ChatGetter.Instance.StartChat("#DEFLID");
            }

            _mainSlider.onValueChanged.RemoveListener(delegate { SetSliderComplete(); });
        }
    }

    public void Reset()
    {
        _mainSlider.value = 0;
    }
}

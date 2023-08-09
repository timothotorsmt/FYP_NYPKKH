using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MinigameBase;
using UniRx;

public class OcclusionHints : HintSystemBase
{

    void Start()
    {
        // used as kinda of an event system
        // Whenever they change state (player knows what they are doing)
        // Reset the hint timer
        OcclusionTaskController.Instance.CurrentTask.Value.Subscribe(state => {
            if (_isRunningHint && !_isDisabled)
            {
                StopCoroutine(HintCounter());
            }

            if (state == OcclusionTasks.INFORM_STAFF_NURSE)
            {
                _button.SetActive(true);
            }
            else if (!_isDisabled)
            {
                StartCoroutine(HintCounter());
            }
        });
    }

    public void GetHint()
    {
        _button.SetActive(false);

        switch (OcclusionTaskController.Instance.GetCurrentTask())
        {
            case OcclusionTasks.MUTE_ALARM:
            case OcclusionTasks.OPEN_ROLLER_CLAMP:
                _chatGetter.DisplayChatLine("#OCCLRB");
                break;
            case OcclusionTasks.UNCLAMP_T_CONNECTOR:
            case OcclusionTasks.UNKINK_LINE:
            case OcclusionTasks.ASSESS_SKIN:
                _chatGetter.DisplayChatLine("#OCCLGA");
                break;
            case OcclusionTasks.INFORM_STAFF_NURSE:
                _chatGetter.DisplayChatLine("#OCCLPB");
                OcclusionTaskController.Instance.MarkCurrentTaskAsDone();
                Debug.Log("Hu");
                break;
        }
    }
}

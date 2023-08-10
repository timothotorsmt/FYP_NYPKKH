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
                _chatGetter.DisplayChatLine("#OCCLHA");
                break;
            case OcclusionTasks.OPEN_ROLLER_CLAMP:
                _chatGetter.DisplayChatLine("#OCCLRB");
                break;
            case OcclusionTasks.UNCLAMP_T_CONNECTOR:
                _chatGetter.DisplayChatLine("#OCCLTA");
                break;
            case OcclusionTasks.UNKINK_LINE:
                _chatGetter.DisplayChatLine("#OCCLKA");
                break;
            case OcclusionTasks.ASSESS_SKIN:
                _chatGetter.DisplayChatLine("#OCCLPE");
                break;
            case OcclusionTasks.START_PUMP:
                _chatGetter.DisplayChatLine("#PERIHM");
                break;
            case OcclusionTasks.CLAMP_T_CONNECTOR:
            case OcclusionTasks.PUT_PUMP_ON_STANDBY:
                _chatGetter.DisplayChatLine("#OCCLHB");
                break;
            case OcclusionTasks.DISCONNECT_TUBING_FROM_PLUG:
                _chatGetter.DisplayChatLine("#OCCLHC");
                break;
            case OcclusionTasks.REMOVE_TEGADERM:
            case OcclusionTasks.PULL_OUT_PLUG:
                _chatGetter.DisplayChatLine("#OCCLHD");
                break;
            case OcclusionTasks.PUT_PLASTER:
                _chatGetter.DisplayChatLine("#OCCLHE");
                break;
            case OcclusionTasks.INFORM_STAFF_NURSE:
                _chatGetter.DisplayChatLine("#OCCLPB");
                
                OcclusionTaskController.Instance.MarkCurrentTaskAsDone();
                break;
        }
    }
}

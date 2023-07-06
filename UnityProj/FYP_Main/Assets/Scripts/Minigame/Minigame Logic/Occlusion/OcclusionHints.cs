using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionHints : MonoBehaviour
{
    [SerializeField] private MinigameChatGetter _chatGetter;

    public void GetHint()
    {
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
                break;
        }
    }
}

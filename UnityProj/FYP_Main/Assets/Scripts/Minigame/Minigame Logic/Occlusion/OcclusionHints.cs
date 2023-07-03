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
            case OcclusionTasks.INFORM_STAFF_NURSE:
                _chatGetter.DisplayChatLine("#OCCLPB");
                OcclusionTaskController.Instance.MarkCurrentTaskAsDone();
                break;
        }
    }
}

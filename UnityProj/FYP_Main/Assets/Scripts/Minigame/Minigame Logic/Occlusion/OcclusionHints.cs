using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionHints : MonoBehaviour
{
    public void GetHint()
    {
        switch (OcclusionTaskController.Instance.GetCurrentTask())
        {
            case OcclusionTasks.INFORM_STAFF_NURSE:
                OcclusionTaskController.Instance.MarkCurrentTaskAsDone();
                break;
        }
    }
}

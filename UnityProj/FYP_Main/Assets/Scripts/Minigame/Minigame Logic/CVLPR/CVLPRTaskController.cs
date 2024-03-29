using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.DesignPatterns;
using UniRx.Extention;
using UnityEngine.Events;
using UniRx;


public class CVLPRTaskController : MinigameTaskController<CVLPRTasks>
{
    void Start()
    {
        _startEvent.Invoke();
    }
}

// The tasks that need to be completed for this 
public enum CVLPRTasks
{
    // Optional tasks
    // Collect prerequisites
    PRERQUISITES,
    NUM_MANDATORY_TASKS,

    NUM_TASKS
}
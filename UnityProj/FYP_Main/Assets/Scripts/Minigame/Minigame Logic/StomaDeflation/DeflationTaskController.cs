using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.DesignPatterns;
using UniRx.Extention;
using UnityEngine.Events;
using UniRx;


public class DeflationTaskController : MinigameTaskController<DeflationTasks>
{
    void Start()
    {
        _startEvent.Invoke();
    }
}

// The tasks that need to be completed for this 
public enum DeflationTasks
{
    DEFLATE_BAGS,

    TIMER_OVER,

    NUM_MANDATORY_TASKS,

    UNCLAMP_BAG,
    UNROLL_BAG,
    RELEASE_AIR,

    NUM_TASKS
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.DesignPatterns;
using UniRx.Extention;
using UnityEngine.Events;
using UniRx;


public class DeflationProcedureTaskController : MinigameTaskController<DeflationProcedureTasks>
{
    void Start()
    {
        _startEvent.Invoke();
    }

    private void Update()
    {
    }
}

// The tasks that need to be completed for this 
public enum DeflationProcedureTasks
{
    UNCLIP_BAG,
    UNROLL_BAG,
    RELEASE_AIR,
    ROLL_BAG,
    CLIP_BAG,

    NUM_MANDATORY_TASKS,

    CLEAN_UP_POO,

    NUM_TASKS
}
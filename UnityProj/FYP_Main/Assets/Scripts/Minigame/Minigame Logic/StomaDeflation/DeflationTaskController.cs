using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.DesignPatterns;
using UniRx.Extention;
using UnityEngine.Events;
using UniRx;


public class DeflationTaskController : MinigameTaskController<DeflationTasks>
{
    public UnityEvent _startGame;
    void Start()
    {
        // _startEvent.Invoke();
        ChatGetter.Instance.StartChat("#STOMAA", _startGame);
    }

    private void Update()
    {
    }
}

// The tasks that need to be completed for this 
public enum DeflationTasks
{
    DEFLATE_BAGS,

    NUM_MANDATORY_TASKS,

    UNCLIP_BAG,
    UNROLL_BAG,
    RELEASE_AIR,
    ROLL_BAG,
    CLIP_BAG,

    NUM_TASKS
}
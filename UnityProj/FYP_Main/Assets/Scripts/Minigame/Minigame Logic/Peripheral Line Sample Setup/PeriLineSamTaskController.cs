using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.DesignPatterns;
using UniRx.Extention;
using UnityEngine.Events;
using UniRx;

// Controls the main game logic behind the peripheral line sample setup minigame
public class PeriLineSamTaskController : MinigameTaskController<PeriLineSamTasks>
{
    // empty bc we dont actually need anything LOL
}

// The tasks that need to be completed for this 
public enum PeriLineSamTasks
{
    CLOSE_CLAMP = 0,
    REMOVE_SPIKE_IV_TUBE,
    INSERT_SPIKE,
    SQUEEZE_BAG,
    OPEN_ROLLER_CLAMP,
    NUM_MANDATORY_TASKS,
    // Optional Tasks
    // Optional?? Maybe dont add 
    TAP_CHAMBER,

    // Troubleshooting segement(?)
    FLICK_LINE,
    NUM_TASKS
}
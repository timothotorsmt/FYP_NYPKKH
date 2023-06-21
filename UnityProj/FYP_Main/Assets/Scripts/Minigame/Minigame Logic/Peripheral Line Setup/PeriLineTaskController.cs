using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.DesignPatterns;
using UniRx.Extention;
using UnityEngine.Events;
using UniRx;

// Controls the main game logic behind the peripheral line setup minigame
public class PeriLineTaskController : MinigameTaskController<PeriLineTasks>
{
    
}

// The tasks that need to be completed for this 
public enum PeriLineTasks
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
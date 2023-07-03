using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.DesignPatterns;
using UniRx.Extention;
using UnityEngine.Events;
using UniRx;


public class PeripheralSetupTaskController : MinigameTaskController<PeripheralSetupTasks>
{
    void Start()
    {
        SetFirstTask();
        _startEvent.Invoke();
    }
    
    public void OnFinishIntro()
    {
        // TODO: call this after intro sequence
        MarkCurrentTaskAsDone();
    }
}

// The tasks that need to be completed for this 
public enum PeripheralSetupTasks
{
    INTRO,
    PEEL_OFF_FOIL, // Confirm what the fuck foil is tomorrow
    CLEAN_WITH_SWAB,
    OPEN_INFUSION_TUBING,
    DROP_INFUSION_ON_TABLE,
    CLAMP_ROLLER_CLAMP,
    REMOVE_PROTECTIVE_CAP,
    SPIKE_INFUSION_BOTTLE,
    PRIME_INFUSION_TUBING,

    OPEN_ROLLER_CLAMP,
    OPEN_DOOR,
    ATTACH_TO_PUMP,
    CLOSE_DOOR,
    SET_PUMP_PARAMETER,

    CLEAN_IV_CLAVE,
    FLUSH_IV_PLUG_CLAVE,
    CONNECT_INFUSION_TUBING,
    START_PUMP,
    NUM_MANDATORY_TASKS,

    FLICK_LINE,
    NUM_TASKS,
    UNCLAMP_T_CONNECTOR,

}
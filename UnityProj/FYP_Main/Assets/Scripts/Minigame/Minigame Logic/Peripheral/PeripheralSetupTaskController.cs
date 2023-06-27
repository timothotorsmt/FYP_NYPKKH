using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.DesignPatterns;
using UniRx.Extention;
using UnityEngine.Events;
using UniRx;


public class PeripheralSetupTaskController : MinigameTaskController<PeripheralSetupTasks>
{

}

// The tasks that need to be completed for this 
public enum PeripheralSetupTasks
{
    INTRO,
    PEEL_OFF_FOIL, // Confirm what the fuck foil is tomorrow
    CLEAN_WITH_SWAB,
    OPEN_INFUSION_TUBING,
    CLAMP_ROLLER_CLAMP,
    REMOVE_PROTECTIVE_CAP,
    REMOVE_PROTECTIVE_SEAL,
    SPIKE_INFUSION_BOTTLE,
    OPEN_ROLLER_CLAMP,
    PRIME_INFUSION_TUBING,
    OPEN_DOOR,
    ATTACH_TO_PUMP,
    CLOSE_DOOR,
    CLEAN_IV_CLAVE,
    FILL_SYRINGE,
    FLUSH_IV_PLUG_CLAVE,
    CONNECT_INFUSION_TUBING,
    SET_PUMP_PARAMETER,
    START_PUMP,
    UNCLAMP_T_CONNECTOR,
    NUM_MANDATORY_TASKS,

    FLICK_LINE,
    NUM_TASKS
}
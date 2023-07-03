using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.DesignPatterns;
using UniRx.Extention;
using UnityEngine.Events;
using UniRx;


public class OcclusionTaskController : MinigameTaskController<OcclusionTasks>
{

}

// The tasks that need to be completed for this 
public enum OcclusionTasks
{
    // Mute alarm
    MUTE_ALARM,

    // Optional tasks
    // Scenario 1: Roller clamp
    OPEN_ROLLER_CLAMP,

    // Scenario 2: Kinked lines
    UNKINK_LINE,

    // Scenario 3: T-Connector
    UNCLAMP_T_CONNECTOR,

    // Scenario 4: 3 Way tap
    FIX_3WAY_TAP,

    // Scenario 5: Phlebitis
    ASSESS_SKIN,
    CLAMP_T_CONNECTOR,
    PUT_PUMP_ON_STANDBY,
    INFORM_STAFF_NURSE,

    DISCONNECT_TUBING_FROM_PLUG,
    REMOVE_TEGADERM,
    PULL_OUT_PLUG,
    USE_GAUZE,
    PUT_PLASTER,

    START_PUMP,

    NUM_MANDATORY_TASKS,

    NUM_TASKS
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.DesignPatterns;
using UniRx.Extention;
using UnityEngine.Events;
using UniRx;

// Controls the main game logic behind the peripheral line sample setup minigame
public class CVLItemTaskController : MinigameTaskController<CVLItemTasks>
{
    // empty bc we dont actually need anything LOL
}

// The tasks that need to be completed for this 
public enum CVLItemTasks
{
    UNSTERILE,
    NUM_TASKS
}
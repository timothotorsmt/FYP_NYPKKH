using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.DesignPatterns;
using UniRx.Extention;
using UnityEngine.Events;
using UniRx;

// Controls the main game logic behind the peripheral line setup minigame
public class PeriLineControl : Singleton<PeriLineControl>
{
    #region variables
    public ReactiveProp<PeriLineTasks> CurrentTask = new ReactiveProp<PeriLineTasks>();
    // this function keeps the next task in ready for temporary / optional tasks
    private PeriLineTasks _nextTask;
    [SerializeField] private UnityEvent _startEvent;
    private int TaskCount;


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Put the first task as the current task
        CurrentTask.SetValue((PeriLineTasks)0);
        _nextTask = (PeriLineTasks)1;

        _startEvent.Invoke();
    }

    public PeriLineTasks GetCurrentTask()
    {
        
        return CurrentTask.GetValue();
    }

    // Move to next task
    public void MarkCurrentTaskAsDone()
    {
        // Check if all tasks have been completed
        if (_nextTask == PeriLineTasks.NUM_MANDATORY_TASKS)
        {
            // End of game
            // Maybe chat will be activated or whatever
            EndGame();
            return;
        }
        
        // Count the next task
        int index = (int)_nextTask + 1;
        // assign the next task to the current task
        CurrentTask.SetValue(_nextTask);
        // increase the next task
        _nextTask = (PeriLineTasks)index;
        Debug.Log(CurrentTask.GetValue().ToString());
    }

    public void AssignTasks(PeriLineTasks newTask)
    {
        // Put the next task on hold; make sure the temporary task is done first
        // once new task is done, continue with previous task
        _nextTask = CurrentTask.GetValue();
        CurrentTask.SetValue(newTask);
    }

    public void EndGame()
    {
        MinigameSceneController.Instance.EndMinigame();
    }
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
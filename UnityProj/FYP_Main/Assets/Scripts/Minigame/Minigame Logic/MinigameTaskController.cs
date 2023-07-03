using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.DesignPatterns;
using UniRx.Extention;
using UnityEngine.Events;
using UniRx;

// Controls the main game logic behind the peripheral line setup minigame
public class MinigameTaskController<TaskType> : Singleton<MinigameTaskController<TaskType>> where TaskType : struct, System.Enum
{
    #region variables
    public MinigamePerformance CurrentMinigamePerformance;
    public ReactiveProp<TaskType> CurrentTask = new ReactiveProp<TaskType>();
    // this function keeps the next task in ready for temporary / optional tasks
    protected TaskType _nextTask;
    // Events that take place before start and end of the game
    [SerializeField] protected UnityEvent _startEvent;
    [SerializeField] protected UnityEvent _finishEvent;
    protected int TaskCount;
    #endregion

    public TaskType GetCurrentTask()
    {
        
        return CurrentTask.GetValue();
    }

    // Move to next task
    public void MarkCurrentTaskAsDone()
    {
        if (CheckIfGameOver()) { return; }
        
        // Count the next task
        int index = (int)(object)(_nextTask) + 1;
        // assign the next task to the current task
        CurrentTask.SetValue(_nextTask);
        // increase the next task
        _nextTask = (TaskType)(object)index;
        Debug.Log(CurrentTask.GetValue().ToString());

        CurrentMinigamePerformance.AddPositiveAction();
    }

    protected void SetFirstTask()
    {
        // Put the first task as the current task
        CurrentTask.SetValue((TaskType)(object)0);
        _nextTask = (TaskType)(object)1;
    }

    private bool CheckIfGameOver()
    {
        bool exists = System.Enum.IsDefined(typeof(TaskType), "NUM_MANDATORY_TASKS");

        if (exists) 
        {
            // Check if all tasks have been completed
            if (_nextTask.ToString() == "NUM_MANDATORY_TASKS")
            {
                // End of game
                // Maybe chat will be activated or whatever
                EndGame();
                return true;
            }
        }
        return false;
    }

    public void AssignTasks(TaskType newTask)
    {
        // Put the current task on hold; make sure the temporary task is done first
        // once new task is done, continue with previous task
        _nextTask = CurrentTask.GetValue();
        CurrentTask.SetValue(newTask);
    }

    public void AssignNextTaskContinuous(TaskType newTask)
    {
        // Override the order of the next task, change to a different task 
        _nextTask = newTask;
    }

    public void AssignCurrentTaskContinuous(TaskType newTask)
    {
        CurrentTask.SetValue(newTask);
        int index = (int)(object)(newTask) + 1;
        _nextTask = (TaskType)(object)index;
    }

    public void EndGame()
    {
        // Display the performance review screen
        //CurrentMinigamePerformance.EvaluatePerformance();
        //MinigameSceneController.Instance.EndMinigame();
        _finishEvent.Invoke();
    }
}

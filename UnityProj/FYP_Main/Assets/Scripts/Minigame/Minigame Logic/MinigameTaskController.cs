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
    public ReactiveProp<TaskType> CurrentTask = new ReactiveProp<TaskType>();
    // this function keeps the next task in ready for temporary / optional tasks
    private TaskType _nextTask;
    // Events that take place before start and end of the game
    [SerializeField] private UnityEvent _startEvent;
    [SerializeField] private UnityEvent _finishEvent;
    private int TaskCount;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SetFirstTask();

        _startEvent.Invoke();
    }

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
                _finishEvent.Invoke();
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

    public void EndGame()
    {
        MinigameSceneController.Instance.EndMinigame();
    }
}

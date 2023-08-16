using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.DesignPatterns;
using UniRx.Extention;
using UnityEngine.Events;
using UniRx;

// This class contains code to control the whole task system
// The task system is what is used to control the whole minigame
public class MinigameTaskController<TaskType> : Singleton<MinigameTaskController<TaskType>> where TaskType : struct, System.Enum
{
    #region variables

    public ReactiveProp<TaskType> CurrentTask = new ReactiveProp<TaskType>();
    // this function keeps the next task in ready for temporary / optional tasks
    protected TaskType _nextTask;
    // Events that take place before start and end of the game
    [SerializeField] protected UnityEvent _startEvent;
    [SerializeField] protected UnityEvent _finishEvent;

    private MinigamePerformance _currentMinigamePerformance;


    #endregion

    private void Start()
    {
        _currentMinigamePerformance = MinigamePerformance.Instance;
    }

    /// <summary>
    /// Get the current Task that it is on
    /// </summary>
    /// <returns></returns>
    public TaskType GetCurrentTask()
    {
        return CurrentTask.GetValue();
    }

    /// <summary>
    /// Marks the current task as done, and then moves onto the next task
    /// Also checks if the game is over and triggers the end minigame
    /// </summary>
    /// <param name="PlayParticleEffect"> Enables/Disables the particle effects </param>
    public void MarkCurrentTaskAsDone(bool PlayParticleEffect = true)
    {
        if (CheckIfGameOver()) { return; }
        
        // Count the next task
        int index = (int)(object)(_nextTask) + 1;
        // assign the next task to the current task
        CurrentTask.SetValue(_nextTask);
        // increase the next task
        _nextTask = (TaskType)(object)index;

        Debug.Log(CurrentTask.GetValue().ToString());


        if (PlayParticleEffect)
        {
            MarkCorrectTask();
        }
    }

    #region Points Management

    /// <summary>
    /// Adds points to the scoreboard
    /// </summary>
    /// <param name="PlayParticleEffect"> Enable/Disables the "good" effect </param>
    public void MarkCorrectTask(bool PlayParticleEffect = true)
    {
        // Add a positive reaction <3 
        if (_currentMinigamePerformance == null)
        {
            _currentMinigamePerformance = MinigamePerformance.Instance;
        }
        _currentMinigamePerformance.AddPositiveAction(PlayParticleEffect);
    }

    /// <summary>
    /// Removes Points from the Scoreboard
    /// </summary>
    /// <param name="PlayParticleEffect"> Enable/Disables the "good" effect </param>
    public void MarkWrongTask(bool PlayParticleEffect = true)
    {
        // Add a positive reaction <3 
        if (_currentMinigamePerformance == null)
        {
            _currentMinigamePerformance = MinigamePerformance.Instance;
        }
        _currentMinigamePerformance.AddNegativeAction(PlayParticleEffect);
    }

    #endregion

    /// <summary>
    /// Set the first task as the current task
    /// </summary>
    protected void SetFirstTask()
    {
        CurrentTask.SetValue((TaskType)(object)0);
        _nextTask = (TaskType)(object)1;
    }

    /// <summary>
    /// Check if the task "NUM_MANDATORY_TASKS" exists
    /// If it does, and the next task is num mandatory tasks, then end the game
    /// </summary>
    /// <returns> Returns whether the minigame has ended or not </returns>
    private bool CheckIfGameOver()
    {
        bool exists = System.Enum.IsDefined(typeof(TaskType), "NUM_MANDATORY_TASKS");

        if (exists) 
        {
            // Check if all mandatory tasks have been completed
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

    #region Functions to override the current task

    /// <summary>
    /// Put the current task on hold; make sure the temporary task is done first
    /// once new task is done, continue with previous task
    /// </summary>
    /// <param name="newTask"> Task to Override </param>
    public void AssignTasks(TaskType newTask)
    {
        
        _nextTask = CurrentTask.GetValue();
        CurrentTask.SetValue(newTask);
    }

    /// <summary>
    /// Override the order of the next task, change to a different task 
    /// </summary>
    /// <param name="newTask"> Task To Override </param>
    public void AssignNextTaskContinuous(TaskType newTask)
    {
        _nextTask = newTask;
    }

    /// <summary>
    /// Override the current task, set that to this new task'
    /// Next few tasks will be continuing off this task
    /// </summary>
    /// <param name="newTask"> New task to override </param>
    public void AssignCurrentTaskContinuous(TaskType newTask)
    {
        CurrentTask.SetValue(newTask);
        int index = (int)(object)(newTask) + 1;
        _nextTask = (TaskType)(object)index;
    }

    #endregion

    /// <summary>
    /// End the current minigame.
    /// </summary>
    public void EndGame()
    {
        // Display the performance review screen
        _currentMinigamePerformance.EvaluatePerformance();
    }
}

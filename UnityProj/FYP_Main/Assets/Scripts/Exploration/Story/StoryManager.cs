using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.DesignPatterns;
using UniRx.Extention;
using UnityEngine.Events;
using UniRx;
using UnityEngine.Rendering;

public class StoryManager<StoryBeat> : Singleton<StoryManager<StoryBeat>> where StoryBeat : struct, System.Enum
{
    public ReactiveProp<StoryBeat> CurrentStoryBeat = new ReactiveProp<StoryBeat>();
    // this function keeps the next task in ready for temporary / optional tasks
    protected StoryBeat _nextStoryBeat;

    // Start is called before the first frame update
    void Start()
    {
        SetFirstStoryBeat();
    }

    public StoryBeat GetCurrentStoryBeat()
    {
        return CurrentStoryBeat.GetValue();
    }

    // Move to next task
    public void MarkCurrentStoryBeatAsDone()
    {
        if (CheckIfGameOver()) { return; }

        // Count the next task
        int index = (int)(object)(_nextStoryBeat) + 1;
        // assign the next task to the current task
        CurrentStoryBeat.SetValue(_nextStoryBeat);
        // increase the next task
        _nextStoryBeat = (StoryBeat)(object)index;
    }

    protected void SetFirstStoryBeat()
    {
        // Put the first task as the current task
        CurrentStoryBeat.SetValue((StoryBeat)(object)0);
        _nextStoryBeat = (StoryBeat)(object)1;
    }

    private bool CheckIfGameOver()
    {
        bool exists = System.Enum.IsDefined(typeof(StoryBeat), "NUM_STORY_BEATS");

        if (exists)
        {
            // Check if all mandatory tasks have been completed
            if (_nextStoryBeat.ToString() == "NUM_STORY_BEATS")
            {
                // End of game
                // Maybe chat will be activated or whatever
                return true;
            }
        }
        return false;
    }

    public void SetNewStoryBeat(StoryBeat newStoryBeat)
    {
        CurrentStoryBeat.SetValue(newStoryBeat);
        _nextStoryBeat = (StoryBeat)(object)((int)(object)newStoryBeat + 1);


    }

}

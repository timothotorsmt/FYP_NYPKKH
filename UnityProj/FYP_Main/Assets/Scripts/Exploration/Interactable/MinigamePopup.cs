using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChatSys;
using UnityEngine.Events;

public class MinigamePopup : Interactable
{
    [SerializeField] private MinigameID _idMinigame;
    [SerializeField] private Difficulty _difficulty;

    [SerializeField] private UnityEvent _ChecklistEvent; // Checklist event is for what? If it is for CVL pre req, don't put it in this class.
    [SerializeField] private string _idChat; // Why is there a chat ID for minigame popup? Inherit from this to make a new class for this functionality
    bool goMiniGame; // What does this do
    bool test = false;  // What does this do

    public override void Interact()
    {
        // Actual required code
        MinigameManager.Instance.StartMinigame(_idMinigame, _difficulty);

        //ChatGetter.Instance.StartChat(_idChat, _ChecklistEvent);
        //test = true;
    }

    // ** try to avoid update as much as possible
    private void Update()
    {
        //stuff(); //Why is this constantly running
    }

    // Properly name your functions
    public void stuff()
    {
        if ( test && ChatGetter.Instance.getChatID() == "#CVLPRF")
        {
            goMiniGame = true;
        }
    }
    public void Test()
    {
        test = false;

        // Why is there an if statement with no brackets
        //if (goMiniGame)
    }
}

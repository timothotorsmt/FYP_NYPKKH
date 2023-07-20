using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChatSys;
using UnityEngine.Events;

public class MinigamePopup : Interactable
{
    [SerializeField] private MinigameID _idMinigame;
    [SerializeField] private Difficulty _difficulty;

    [SerializeField] private UnityEvent _ChecklistEvent;
    [SerializeField] private string _idChat;
    bool goMiniGame;
    bool test = false;  

    public override void Interact()
    {
        ChatGetter.Instance.StartChat(_idChat, _ChecklistEvent);
        test = true;
       
    }

    private void Update()
    {
        stuff();
    }
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
        if (goMiniGame)
        MinigameManager.Instance.StartMinigame(_idMinigame, _difficulty);
    }
}

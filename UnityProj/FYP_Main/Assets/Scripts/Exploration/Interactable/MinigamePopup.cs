using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChatSys;

public class MinigamePopup : Popup
{
    [SerializeField] private MinigameID _idMinigame;

    public override void Interact()
    {
        MinigameManager.Instance.StartMinigame(_idMinigame);
    }
}

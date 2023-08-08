using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChatSys;
using UnityEngine.Events;

public class BossPopup : Interactable
{
    [SerializeField] private MinigameID _idMinigame;


    public override void Interact()
    {
        // Actual required code
        MinigameManager.Instance.StartBossMinigame(_idMinigame);
    }
}

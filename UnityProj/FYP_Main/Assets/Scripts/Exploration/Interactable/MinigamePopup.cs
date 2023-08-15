using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChatSys;
using UnityEngine.Events;

public class MinigamePopup : Interactable
{
    [SerializeField] private MinigameID _idMinigame;
    [SerializeField] private Difficulty _difficulty;
    [SerializeField] private GameObject _player;

    public override void Interact()
    {
        // Actual required code
        MinigameManager.Instance.StartMinigame(_idMinigame, _difficulty);

        // Save the player position
        PlayerDataSaver.playerData.playerPos = _player.transform.position;
        PlayerDataSaver.SaveCurrentData();
    }
}

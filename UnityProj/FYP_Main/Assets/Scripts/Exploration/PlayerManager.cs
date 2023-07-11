using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Extention;

public class PlayerManager : MonoBehaviour
{
    public ReactiveProp<PlayerState> CurrentPlayerState;
    private Room _currentRoom;

    public void SetCurrentRoom(Room newRoom, Direction newDirection)
    {
        _currentRoom = newRoom;
    }
}

public enum PlayerState 
{
    PAUSED,
    IDLE,
    MOVING
}

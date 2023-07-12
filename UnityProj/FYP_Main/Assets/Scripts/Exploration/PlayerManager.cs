using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Extention;
using Common.DesignPatterns;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private PlayerRoomInfo _playerRoomInfo;

    public void ChangeRoom(Door _doorToChange, Direction _newDirection)
    {
        _playerMovement.GoToDoor(_doorToChange);
        _playerRoomInfo.ChangeRoom(_doorToChange._room);
        _cameraController.SetToNewRoom();
    }
}

public enum PlayerState 
{
    PAUSED,
    IDLE,
    MOVING
}

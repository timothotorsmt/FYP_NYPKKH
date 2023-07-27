using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Extention;
using Common.DesignPatterns;

// Controls the overall behaviours of the player
// Also stores all the components of the player
public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private PlayerMovement _playerMovement; // Controls movement
    [SerializeField] private CameraController _cameraController; // Controls camera
    [SerializeField] private PlayerAnimationController _playerAnimationController; // Controls the animation
    [SerializeField] private PlayerRoomInfo _playerRoomInfo; // Contains information about the room the player currently is in
    
    public OverallStoryController _overallStoryController; // Contains information about the room the player currently is in

    public void Start()
    {
        Init();
    }

    // Initialise certain things
    private void Init()
    {
        // Not using interface because player movement needs to be initialised before player animation
        _playerMovement.Init();
        _playerAnimationController.Init(_playerMovement);
    }

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

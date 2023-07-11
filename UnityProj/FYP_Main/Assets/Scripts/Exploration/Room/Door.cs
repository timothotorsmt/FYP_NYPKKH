using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Door _connectingDoor;
    [SerializeField] private Room _currentRoom;

    [SerializeField] private Direction _direction;
    [SerializeField] private PlayerManager _playerManager;

    public void EnterDoor()
    {
        _playerManager.SetCurrentRoom(_currentRoom, _direction);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Door _connectingDoor;
    [SerializeField] public Room _room;

    public Direction _doorDirection;

    // Change room
    public void ChangeRoom()
    {
        PlayerManager.Instance.ChangeRoom(_connectingDoor, _doorDirection);
    }
}

public enum Direction
{
    LEFT,
    RIGHT,
    FORWARDS,
    BACKWARDS
}

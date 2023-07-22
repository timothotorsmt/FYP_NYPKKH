using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Door.
public class Door : MonoBehaviour
{
    [Header("Door Settings")]
    // Other side of the door
    [Tooltip("Connecting door")] [SerializeField] private Door _connectingDoor;
    // Room it belongs to
    [Tooltip("Current Room it belongs to")] [SerializeField] public Room _room;
    [Tooltip("Where the player would be spawned")] [SerializeField] private GameObject _spawnPoint;

    public Direction _doorDirection;

    // Change room
    public void ChangeRoom()
    {
        PlayerManager.Instance.ChangeRoom(_connectingDoor, _doorDirection);
    }

    public Vector2 GetSpawnPoint()
    {
        return _spawnPoint.transform.position;
    }
}

public enum Direction
{
    LEFT,
    RIGHT,
    FORWARDS,
    BACKWARDS
}

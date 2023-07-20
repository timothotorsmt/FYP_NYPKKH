using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Door _connectingDoor;
    [SerializeField] public Room _room;
    [SerializeField] private GameObject _spawnPoint;

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

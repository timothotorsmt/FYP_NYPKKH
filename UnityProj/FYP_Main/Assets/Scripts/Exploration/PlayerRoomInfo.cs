using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoomInfo : MonoBehaviour
{
    [SerializeField] public Room _currentRoom;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ChangeRoom(Room newRoom)
    {
        _currentRoom = newRoom;
        Debug.Log(newRoom.RoomName);
    }
}

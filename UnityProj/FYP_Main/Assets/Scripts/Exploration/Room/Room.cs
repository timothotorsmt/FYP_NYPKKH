using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private string _RoomName;
    [SerializeField] private string _SectionName;

    private void Start()
    {
    }
}

public enum Direction
{
    LEFT,
    RIGHT,
    FRONT,
    BACK,
    UP,
    DOWN,
}



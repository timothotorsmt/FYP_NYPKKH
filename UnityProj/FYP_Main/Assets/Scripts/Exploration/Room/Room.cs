using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Room : MonoBehaviour
{
    [SerializeField] private string _roomName;
    [SerializeField] private string _sectionName;

    public string RoomName => _roomName;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public float GetRoomMinEdge()
    {
        return (_spriteRenderer.bounds.min.x);
    }

    public float GetRoomMaxEdge()
    {
        return (_spriteRenderer.bounds.max.x);
    }
}

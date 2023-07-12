using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Extention;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
}

public enum PlayerState 
{
    PAUSED,
    IDLE,
    MOVING
}

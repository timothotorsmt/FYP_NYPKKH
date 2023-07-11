using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Extention;

public class PlayerManager : MonoBehaviour
{
    public ReactiveProp<PlayerState> CurrentPlayerState;

}

public enum PlayerState 
{
    PAUSED,
    IDLE,
    MOVING
}

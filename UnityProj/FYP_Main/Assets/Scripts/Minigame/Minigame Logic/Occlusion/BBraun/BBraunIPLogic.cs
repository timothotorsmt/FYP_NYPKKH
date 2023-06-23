using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Extention;

// This class simulates the functionality of a BBraun infusion pump. (BBraun IP (i just like the name bbraun its very fun to say))
// The functionality of the pump is based off the official BBraun infusion pump manual
// Functionality =/= aesthetics :3
// Fuck
public class BBraunIPLogic : MonoBehaviour
{
    [SerializeField] private BBraunIPUIDisplay _bBraunIPUIDisplay;
    [SerializeField] private BBraunIPInput _bBraunIPInput;
    [SerializeField] private ReactiveProp<BBraunIPState> _bBraunIPState;

    // uusually is things affect IP, but for occlusion minigame is the other way around teehee
    #region Inverse IP behavior

    // Roller clamp
    

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _bBraunIPState = new ReactiveProp<BBraunIPState>();
    }

    public void SetBBraunAlarm(BBraunIPState newState)
    {
        _bBraunIPState.SetValue(newState);
    }
}

// The current state of the BBraun infusion machine
// For alertion of the alarm 
public enum BBraunIPState
{
    NORMAL = 0,
    CHECK_UPSTREAM,
    PRESSURE_HIGH
}
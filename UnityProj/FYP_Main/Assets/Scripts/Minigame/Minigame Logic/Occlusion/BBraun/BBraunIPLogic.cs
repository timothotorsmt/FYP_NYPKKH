using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Extention;

namespace BBraunInfusomat
{
    // This class simulates the functionality of a BBraun infusion pump. (BBraun IP (i just like the name bbraun its very fun to say))
    // The functionality of the pump is based off the official BBraun infusion pump manual
    // Functionality =/= aesthetics :3
    // Fuck
    public class BBraunIPLogic : MonoBehaviour
    {
        [SerializeField] private BBraunIPUIDisplay _bBraunIPUIDisplay;
        [SerializeField] private BBraunIPInput _bBraunIPInput;
        public ReactiveProp<BBraunIPState> BBraunState = new ReactiveProp<BBraunIPState>();

        // uusually is things affect IP, but for occlusion minigame is the other way around teehee
        #region Inverse IP behavior

        // Roller clamp
        

        #endregion

        public void SetBBraunAlarm(BBraunIPState newState)
        {
            BBraunState.SetValue(newState);
            _bBraunIPInput._okButton.onClick.AddListener(delegate { ResolveAlarm(); });
            _bBraunIPInput._resetValueButton.onClick.AddListener(delegate { MuteAlarm(); });
        }

        public void ResolveAlarm()
        {
            // Stop sound
            // Mark task as done 
            OcclusionTaskController.Instance.MarkCurrentTaskAsDone();
            _bBraunIPInput._okButton.onClick.RemoveListener(delegate { ResolveAlarm(); });
            BBraunState.SetValue(BBraunIPState.NORMAL);

        }

        public void MuteAlarm()
        {
            // Stop sound
            // Mark task as done 
            OcclusionTaskController.Instance.MarkCurrentTaskAsDone();
            _bBraunIPInput._resetValueButton.onClick.RemoveListener(delegate { MuteAlarm(); });
            BBraunState.SetValue(BBraunIPState.NORMAL);
        }
    }

    // The current state of the BBraun infusion machine
    // For alertion of the alarm 
    public enum BBraunIPState
    {
        NORMAL = 0,
        CHECK_UPSTREAM,
        PRESSURE_HIGH,
        OPEN_DOOR,
    }
}
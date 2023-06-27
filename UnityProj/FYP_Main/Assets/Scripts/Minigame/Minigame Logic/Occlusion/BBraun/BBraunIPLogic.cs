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

        #region Alarm Behavior

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

        #endregion

        #region normal functionality

        public void InitialiseNormalBehaviour()
        {
            _bBraunIPInput._onOffButton.onClick.AddListener(delegate { BBraunInitSequence(); });
        }

        private void BBraunInitSequence()
        {
            BBraunState.SetValue(BBraunIPState.START);

            // Remove the ability to power on (?)
            _bBraunIPInput._onOffButton.onClick.RemoveListener(delegate { BBraunInitSequence(); });
            // Send UI to do power on self test sequence

        }

        private void InitCloseDoor()
        {
            BBraunState.SetValue(BBraunIPState.CLOSE_DOOR_SCREEN);

            // Do the entire initialisation part
            
        }

        public void OnFinishInitSeq()
        {
            // Add power off functionality (?)

            // Set to waiting
            BBraunState.SetValue(BBraunIPState.WAITING);

            // Add functionality
            _bBraunIPInput._resetValueButton.onClick.AddListener(delegate { WaitForInput(); }); 
            _bBraunIPInput._openDoorButton.onClick.AddListener(delegate { OpenDoorWaitInput(); }); 
        }

        public void WaitForInput()
        {
            // add dialogue in

        }

        private void OpenDoorWaitInput()
        {
            BBraunState.SetValue(BBraunIPState.OPEN_DOOR_INPUT);

            // Remove existing functionality
            _bBraunIPInput._resetValueButton.onClick.RemoveListener(delegate { WaitForInput(); }); 
            _bBraunIPInput._openDoorButton.onClick.RemoveListener(delegate { OpenDoorWaitInput(); }); 

            // Add new functionality
            _bBraunIPInput._upButton.onClick.AddListener(delegate { OpenDoor(); }); 
            _bBraunIPInput._downButton.onClick.AddListener(delegate { WaitForInput(); }); 
        }

        private void OpenDoor()
        {
            if (PeripheralSetupTaskController.Instance != null)
            {
                if (PeripheralSetupTaskController.Instance.CurrentTask.GetValue() == PeripheralSetupTasks.OPEN_DOOR)
                {
                    // Add animation or whatever here
                    PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();
                }
            }
            BBraunState.SetValue(BBraunIPState.CLOSE_DOOR_SCREEN);
        }

        #endregion
    }

    // The current state of the BBraun infusion machine
    // For anything that requires an input (e.g.turning on and whatever)
    public enum BBraunIPState
    {
        NORMAL = 0,        
        // Alarms (because i started on occlusion first)
        CHECK_UPSTREAM,
        PRESSURE_HIGH,
        DOOR_OPEN,

        // Normal functionality (for peripheral)
        START,
        WAITING,
        OPEN_DOOR_INPUT,
        
        CLOSE_DOOR_SCREEN,
        LINE_SELECTION_INPUT,
        PARAM_MAIN_MENU,
        VBTI_KEY_IN,
        TIME_KEY_IN,
    }
}
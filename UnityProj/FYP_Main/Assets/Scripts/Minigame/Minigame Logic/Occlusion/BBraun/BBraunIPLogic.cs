using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Extention;
using System;

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
        private int _paramSelectIndex = 0;
        [SerializeField] private List<int> _VTBI;
        private int _VTBIIndex;
        public int _VBTIValue;
        [SerializeField] private List<int> _time;
        public int _timeValue;
        private int _timeIndex;


        #region Alarm Behavior

        public void SetBBraunAlarm(BBraunIPState newState)
        {
            BBraunState.SetValue(newState);
            // Mute only
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
            if (OcclusionTaskController.Instance.GetCurrentTask() == OcclusionTasks.MUTE_ALARM)
            {
                // Stop sound
                // Mark task as done 
                OcclusionTaskController.Instance.MarkCurrentTaskAsDone();
                _bBraunIPInput._resetValueButton.onClick.RemoveListener(delegate { MuteAlarm(); });
                BBraunState.SetValue(BBraunIPState.NORMAL);
            }
        }

        #endregion

        #region normal functionality

        private void TurnOffMachine()
        {
            BBraunState.SetValue(BBraunIPState.OFF);
        }

        public void InitialiseNormalBehaviour()
        {
            TurnOffMachine();
            _bBraunIPInput._onOffButton.onClick.AddListener(delegate { BBraunInitSequence(); });
        }

        private void BBraunInitSequence()
        {
            BBraunState.SetValue(BBraunIPState.START);
        }

        public void CloseDoor()
        {
            BBraunState.SetValue(BBraunIPState.CLOSE_DOOR_SCREEN);   
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

            // Add new functionality
            _bBraunIPInput._upButton.onClick.AddListener(delegate { OpenDoor(); }); 
            _bBraunIPInput._downButton.onClick.AddListener(delegate { OnFinishInitSeq(); }); 
        }

        public void WaitForLineSelectionInput()
        {
            BBraunState.SetValue(BBraunIPState.LINE_SELECTION_INPUT);
            
            _bBraunIPInput._leftButton.onClick.AddListener(delegate { SelectedLine(); });
        }

        private void SelectedLine()
        {
            BBraunState.SetValue(BBraunIPState.PARAM_MAIN_MENU);
        }
        
        public void SetControlsParamSelect()
        {
            _bBraunIPInput._downButton.onClick.AddListener(delegate { SetParamDown(); });
            _bBraunIPInput._upButton.onClick.AddListener(delegate { SetParamUp(); });

            // Select behavior
            _bBraunIPInput._leftButton.onClick.AddListener(delegate { SetDigitControls(); });
        }

        public void SetDigitControls()
        {
            Debug.Log(_paramSelectIndex);
            if (_paramSelectIndex == 1)
            {
                BBraunState.SetValue(BBraunIPState.VBTI_KEY_IN);
                _VTBIIndex = 0;
            }
            else if (_paramSelectIndex == 2)
            {
                BBraunState.SetValue(BBraunIPState.TIME_KEY_IN);
                _timeIndex = 0;
            }

            _bBraunIPInput.RemoveAllFunctionality();
            _bBraunIPInput._leftButton.onClick.AddListener(delegate { SetLeft(); });
            _bBraunIPInput._rightButton.onClick.AddListener(delegate { SetRight(); });
            _bBraunIPInput._upButton.onClick.AddListener(delegate { SetDigitUp(); });
            _bBraunIPInput._downButton.onClick.AddListener(delegate { SetDigitDown(); });
            _bBraunIPInput._okButton.onClick.AddListener(delegate { SetToBackMainMenu(); });
        }

        private void SetParamDown()
        {
            // Down is up ?? somehow
            _paramSelectIndex++;
            // Hardcoding the values because theres only a few we care about
            _paramSelectIndex = Mathf.Clamp(_paramSelectIndex, 0, 2);
            _bBraunIPUIDisplay.SelectParamList(_paramSelectIndex);
        }

        private void SetParamUp()
        {
            // Up is down ?? somehow
            _paramSelectIndex--;
            // Hardcoding the values because theres only a few we care about
            _paramSelectIndex = Mathf.Clamp(_paramSelectIndex, 0, 2);
            _bBraunIPUIDisplay.SelectParamList(_paramSelectIndex);
        }

        private void SetToBackMainMenu()
        {
            // Calculate the existing stuff
            if (BBraunState.GetValue() == BBraunIPState.VBTI_KEY_IN)
            {
                for (int i = 0; i < _VTBI.Count; i++)
                {
                    _VBTIValue += (int)(_VTBI[i] * Mathf.Pow(10, i));
                }
            }
            else if (BBraunState.GetValue() == BBraunIPState.TIME_KEY_IN)
            {
                for (int i = 0; i < _VTBI.Count; i++)
                {
                    _timeValue += (int)(_time[i] * Mathf.Pow(10, i));
                }
            }
        }

        private void SetDigitUp()
        {
            if (BBraunState.GetValue() == BBraunIPState.VBTI_KEY_IN)
            {
                int currentNumber = _VTBI[_VTBIIndex] + 1;
                if (currentNumber > 9)
                {
                    currentNumber = 0;
                }
                _VTBI[_VTBIIndex] = _bBraunIPUIDisplay.SetDigitUp(_VTBIIndex, currentNumber);
            }
            else if (BBraunState.GetValue() == BBraunIPState.TIME_KEY_IN)
            {
                int currentNumber = _time[_timeIndex] + 1;
                if (currentNumber > 9)
                {
                    currentNumber = 0;
                }
                _time[_timeIndex] = _bBraunIPUIDisplay.SetDigitUp(_timeIndex, currentNumber);
            }
        }

        private void SetDigitDown()
        {
            if (BBraunState.GetValue() == BBraunIPState.VBTI_KEY_IN)
            {
                int currentNumber = _VTBI[_VTBIIndex] - 1;
                if (currentNumber < 0)
                {
                    currentNumber = 9;
                }
                _VTBI[_VTBIIndex] = _bBraunIPUIDisplay.SetDigitUp(_VTBIIndex, currentNumber);
            }
            else if (BBraunState.GetValue() == BBraunIPState.TIME_KEY_IN)
            {
                int currentNumber = _time[_timeIndex] - 1;
                if (currentNumber < 0)
                {
                    currentNumber = 9;
                }
                _time[_timeIndex] = _bBraunIPUIDisplay.SetDigitUp(_timeIndex, currentNumber);
            }
        }

        private void SetLeft()
        {
            if (BBraunState.GetValue() == BBraunIPState.VBTI_KEY_IN)
            {
                _VTBIIndex++;
                _VTBIIndex = Mathf.Clamp(_VTBIIndex, 0, _VTBI.Count -1);
                _bBraunIPUIDisplay.SetDigit(_VTBIIndex);
            }
            else if (BBraunState.GetValue() == BBraunIPState.TIME_KEY_IN)
            {
                _timeIndex++;
                _timeIndex = Mathf.Clamp(_timeIndex, 0, _time.Count - 1);
                _bBraunIPUIDisplay.SetDigit(_timeIndex);

            }
        }

        private void SetRight()
        {
            if (BBraunState.GetValue() == BBraunIPState.VBTI_KEY_IN)
            {
                _VTBIIndex--;
                _VTBIIndex = Mathf.Clamp(_VTBIIndex, 0, _VTBI.Count - 1);
                _bBraunIPUIDisplay.SetDigit(_VTBIIndex);

            }
            else if (BBraunState.GetValue() == BBraunIPState.TIME_KEY_IN)
            {
                _timeIndex--;
                _timeIndex = Mathf.Clamp(_timeIndex, 0, _time.Count - 1);
                _bBraunIPUIDisplay.SetDigit(_timeIndex);

            }
        }

        private void OpenDoor()
        {
            if (PeripheralSetupTaskController.Instance != null)
            {
                if (PeripheralSetupTaskController.Instance.CurrentTask.GetValue() == PeripheralSetupTasks.OPEN_DOOR)
                {
                    _bBraunIPInput.RemoveAllFunctionality();

                    // Add animation or whatever here
                    PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();
                }
            }
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
        OFF,
    }
}
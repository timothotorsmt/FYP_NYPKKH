using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UniRx.Extention;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;
using Unity.Collections.LowLevel.Unsafe;
using System;
using System.Reflection;

namespace BBraunInfusomat
{
    // This class controls the display of a BBraun infusion pump
    // Does not do any thinking, just display only
    // Event based
    public class BBraunIPUIDisplay : MonoBehaviour
    {
        // Variables
        #region IP variables

        [SerializeField] private BBraunIPLogic _bBraunIPLogic; // The current state of the BBraun machine

        public enum BBraunDisplayState
        {
            OFF,

            // for alarm
            NORMAL_VIEW,
            ALARM_VIEW,

            // Normal functionality
            START_UP,
            SELF_TEST,
            START_UP_INFO1,
            START_UP_INFO2,
            START_UP_WAIT_INPUT,
            OPEN_DOOR_INPUT,
            DOOR_OPENING_SCREEN,
            LINE_CHANGE_SCREEN,
            LINE_CHANGE_SCREEN_OPEN_ROLLER,
            LINE_SELECTION_SCREEN,
            MAIN_MENU,
            RATE_KEY_IN,
            VBTI_KEY_IN,
            TIME_KEY_IN
        }

        // Change the sprite of the screen based on the current state of the BBraun machine
        [SerializeField] private List<changableObject<BBraunDisplayState>> _changableObjects; 
        private ReactiveProp<BBraunDisplayState> _displayState = new ReactiveProp<BBraunDisplayState>();

        // Alarm
        [SerializeField] private GameObject _notifAlarmObject;
        [SerializeField] private TextMeshProUGUI _notifAlarmText;

        // Unity Events 
        // Honestly they're not great? but I just wanna get this over with and go home and sleep ok
        [SerializeField] private UnityEvent _OnFinishInit;
        [SerializeField] private UnityEvent _OnDoorClose;
        [SerializeField] private UnityEvent _OnEnterParams;
        [SerializeField] private ParamContainer[] _paramMenuList;
        [SerializeField] private ParamDigitContainer[] _rateDigits;
        [SerializeField] private ParamDigitContainer[] _VTBIDigits;
        [SerializeField] private ParamDigitContainer[] _timeDigits;

        #endregion

        #region custom defined structs for UI

        // This class is for the parameter choosing thing (i.e. the rate vtbi time)
        [System.Serializable]
        public class ParamContainer
        {
            [SerializeField] private Image _container; // The background
            [SerializeField] private TextMeshProUGUI _variableName; // The actual text itself

            public TextMeshProUGUI _info; // The text info showing the variable
            

            public void SetContainerColor(Color newColour)
            {
                if (newColour == Color.black)
                {
                    _container.color = newColour;
                    _info.color = Color.white;
                    _variableName.color = Color.white;

                    
                }
                else if (newColour == Color.white)
                {
                    _container.color = newColour;
                    _info.color = Color.black;
                    _variableName.color = Color.black;
                    
                }
            }
        }

        [System.Serializable]
        public class ParamDigitContainer
        {
            [SerializeField] private Image _container; // The background
            public TextMeshProUGUI _digit; // The parameter showing the digit

            public void SetContainerColor(Color newColour)
            {
                if (newColour == Color.black)
                {
                    _container.color = newColour;
                    _digit.color = Color.white;
                }
                else if (newColour == Color.white)
                {
                    _container.color = newColour;
                    _digit.color = Color.black;
                }
            }

            public int SetDigit(int newDigit)
            {
                _digit.text = newDigit.ToString();
                return newDigit;
            }
        }

        #endregion

        void Start()
        {
            // Subscribe to the reactive property
            _bBraunIPLogic.BBraunState.Value.Subscribe(state => {
                switch (state)
                {
                    case BBraunIPState.OFF:
                        SetDisplayState(BBraunDisplayState.OFF);
                        break;
                    case BBraunIPState.NORMAL:
                        SetDisplayState(BBraunDisplayState.NORMAL_VIEW);
                        break;                    
                    case BBraunIPState.CHECK_UPSTREAM:                    
                    case BBraunIPState.PRESSURE_HIGH:                    
                    case BBraunIPState.DOOR_OPEN:
                        SetAlarmNotif(state);
                        break;
                    case BBraunIPState.START:
                        StartCoroutine(StartInitSeq());
                        break;
                    case BBraunIPState.WAITING:
                        SetDisplayState(BBraunDisplayState.START_UP_WAIT_INPUT);
                        break;
                    case BBraunIPState.OPEN_DOOR_INPUT:
                        SetDisplayState(BBraunDisplayState.OPEN_DOOR_INPUT);
                        break;
                    case BBraunIPState.CLOSE_DOOR_SCREEN:
                        this.gameObject.SetActive(true);
                        _displayState.SetValue(BBraunDisplayState.LINE_CHANGE_SCREEN);
                        StartCoroutine(CloseDoorSeq());
                        break;
                    case BBraunIPState.PARAM_MAIN_MENU:
                        StartCoroutine(SelectedLineSeq());
                        break;
                    case BBraunIPState.RATE_KEY_IN:
                        SetDisplayState(BBraunDisplayState.RATE_KEY_IN);
                        break;
                    case BBraunIPState.VBTI_KEY_IN:
                        SetDisplayState(BBraunDisplayState.VBTI_KEY_IN);
                        break;
                    case BBraunIPState.TIME_KEY_IN:
                        SetDisplayState(BBraunDisplayState.TIME_KEY_IN);
                        break;
                }
            });

            // Subscribe to the current task state
            _displayState.Value.Subscribe(State => {
                if (_changableObjects.Where(s => s.TaskOnChange == State).Select(s => s.GameObjectToChange).Count() > 0) 
                {
                    foreach (var item in _changableObjects)
                    {
                        item.GameObjectToChange.SetActive(false);
                    }

                    _changableObjects.Where(s => s.TaskOnChange == State).Select(s => s.GameObjectToChange).First().SetActive(true);
                }
            });
        }

        private void SetAlarmNotif(BBraunIPState state)
        {
            switch (state)
            {
                case BBraunIPState.CHECK_UPSTREAM:
                    _notifAlarmText.text = "Check Upstream";
                    break;
                case BBraunIPState.PRESSURE_HIGH:
                    _notifAlarmText.text = "Pressure High";
                    break;
            }

            _displayState.SetValue(BBraunDisplayState.ALARM_VIEW);
        }

        private IEnumerator StartInitSeq()
        {
            _displayState.SetValue(BBraunDisplayState.START_UP);

            yield return new WaitForSeconds(0.5f);

            _displayState.SetValue(BBraunDisplayState.OFF);

            yield return new WaitForSeconds(0.5f);

            _displayState.SetValue(BBraunDisplayState.SELF_TEST);


            yield return new WaitForSeconds(3);

            _displayState.SetValue(BBraunDisplayState.START_UP_WAIT_INPUT);
            _OnFinishInit.Invoke();
        }

        private IEnumerator CloseDoorSeq()
        {
            _displayState.SetValue(BBraunDisplayState.LINE_CHANGE_SCREEN);

            yield return new WaitForSeconds(2.0f);

            _displayState.SetValue(BBraunDisplayState.LINE_SELECTION_SCREEN);
            _OnDoorClose.Invoke();
        }

        private IEnumerator SelectedLineSeq()
        {
            if (_displayState.GetValue() == BBraunDisplayState.LINE_SELECTION_SCREEN)
            {
                _displayState.SetValue(BBraunDisplayState.LINE_CHANGE_SCREEN);

                yield return new WaitForSeconds(2.0f);

                _displayState.SetValue(BBraunDisplayState.LINE_CHANGE_SCREEN_OPEN_ROLLER);

                yield return new WaitForSeconds(2.0f);

                
            }
            
            _displayState.SetValue(BBraunDisplayState.MAIN_MENU);
            _OnEnterParams.Invoke();
        }

        private void SetDisplayState(BBraunDisplayState displayState)
        {
            _displayState.SetValue(displayState);
        }

        public void SelectParamList(int index)
        {
            foreach (ParamContainer container in _paramMenuList)
            {
                container.SetContainerColor(Color.black);
            }

            _paramMenuList[index].SetContainerColor(Color.white);
        }

        public int SetDigitUp(int index, int newNum)
        {
            int returnInt = 0;
            switch (_bBraunIPLogic.BBraunState.GetValue())
            {
                case BBraunIPState.VBTI_KEY_IN:
                    returnInt = _VTBIDigits[index].SetDigit(newNum);
                    break;

                case BBraunIPState.TIME_KEY_IN:
                    returnInt = _timeDigits[index].SetDigit(newNum);
                    break;
            }

            return returnInt;
        }

        public void SetDigit(int index)
        {
            switch (_bBraunIPLogic.BBraunState.GetValue())
            {
                case BBraunIPState.VBTI_KEY_IN:
                    foreach (ParamDigitContainer container in _VTBIDigits)
                    {
                        container.SetContainerColor(Color.black);
                    }

                    _VTBIDigits[index].SetContainerColor(Color.white);
                    break;

                case BBraunIPState.RATE_KEY_IN:
                    foreach (ParamDigitContainer container in _rateDigits)
                    {
                        container.SetContainerColor(Color.black);
                    }

                    _rateDigits[index].SetContainerColor(Color.white);
                    break;

                case BBraunIPState.TIME_KEY_IN:
                    foreach (ParamDigitContainer container in _timeDigits)
                    {
                        container.SetContainerColor(Color.black);
                    }

                    _timeDigits[index].SetContainerColor(Color.white);
                    break;
            }
            
        }

        public void SetParam(string numToSet)
        {
            switch (_bBraunIPLogic.BBraunState.GetValue())
            {
                case BBraunIPState.RATE_KEY_IN:
                    _paramMenuList[0]._info.text = numToSet + " ml/h";
                    break;

                case BBraunIPState.VBTI_KEY_IN:
                    _paramMenuList[1]._info.text = numToSet + " ml";
                    break;

                case BBraunIPState.TIME_KEY_IN:
                    _paramMenuList[2]._info.text = numToSet + " h:min";
                    break;
            }
        }

        public void SetRate(float rate)
        {
            // TODO figure out why this isnt working
            string s = String.Format("{0:0.00}", rate);
            _paramMenuList[0]._info.text = s + " ml/h";

        }
    }
}

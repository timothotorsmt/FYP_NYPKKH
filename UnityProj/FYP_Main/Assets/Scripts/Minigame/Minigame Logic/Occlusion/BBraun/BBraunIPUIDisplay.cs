using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UniRx.Extention;
using System.Linq;
using UnityEngine.Events;

namespace BBraunInfusomat
{
    // This class controls the display of a BBraun infusion pump
    // Does not do any thinking, just display only
    // Event based
    public class BBraunIPUIDisplay : MonoBehaviour
    {
        // Variables
        #region IP variables

        [SerializeField] private BBraunIPLogic _bBraunIPLogic;

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
            VBTI_KEY_IN,
            TIME_KEY_IN
        }

        [SerializeField] private List<changableObject<BBraunDisplayState>> _changableObjects;    
        private ReactiveProp<BBraunDisplayState> _displayState = new ReactiveProp<BBraunDisplayState>();

        // Alarm
        [SerializeField] private GameObject _notifAlarmObject;
        [SerializeField] private TextMeshProUGUI _notifAlarmText;
        [SerializeField] private UnityEvent _OnFinishInit;
        [SerializeField] private UnityEvent _OnDoorClose;
        [SerializeField] private UnityEvent _OnEnterParams;

        #endregion

        void Start()
        {
            // Subscribe to the reactive property
            _bBraunIPLogic.BBraunState.Value.Subscribe(state => {
                switch (state)
                {
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
            _displayState.SetValue(BBraunDisplayState.LINE_CHANGE_SCREEN);

            yield return new WaitForSeconds(2.0f);

            _displayState.SetValue(BBraunDisplayState.LINE_CHANGE_SCREEN_OPEN_ROLLER);

            yield return new WaitForSeconds(2.0f);

            _displayState.SetValue(BBraunDisplayState.MAIN_MENU);
            _OnEnterParams.Invoke();

        }

        private void SetDisplayState(BBraunDisplayState displayState)
        {
            _displayState.SetValue(displayState);
        }
    }
}

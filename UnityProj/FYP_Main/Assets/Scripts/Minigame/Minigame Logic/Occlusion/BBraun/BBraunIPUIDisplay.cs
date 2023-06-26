using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UniRx.Extention;
using System.Linq;

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
            NORMAL_VIEW,
            ALARM_VIEW
        }

        [SerializeField] private List<changableObject<BBraunDisplayState>> _changableObjects;    
        private ReactiveProp<BBraunDisplayState> _displayState = new ReactiveProp<BBraunDisplayState>();

        // Alarm
        [SerializeField] private GameObject _notifAlarmObject;
        [SerializeField] private TextMeshProUGUI _notifAlarmText;

        #endregion

        void Start()
        {
            // Subscribe to the reactive property
            _bBraunIPLogic.BBraunState.Value.Subscribe(state => {
                // If normal; just ignore whatevs
                if (state != BBraunIPState.NORMAL)
                {
                    SetAlarmNotif(state);
                }
                else
                {
                    SetDisplayState(BBraunDisplayState.NORMAL_VIEW);
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

        private void SetDisplayState(BBraunDisplayState displayState)
        {
            _displayState.SetValue(displayState);
        }
    }
}

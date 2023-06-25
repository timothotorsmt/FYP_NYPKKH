using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;

namespace BBraunInfusomat
{
    // This class controls the display of a BBraun infusion pump
    // Does not do any thinking, just display only
    // Event based
    public class BBraunIPUIDisplay : MonoBehaviour
    {
        // Variables
        #region IP variables

        private TextMeshProUGUI _notifAlarmText;

        #endregion

        #region UI variables

        private BBraunIPLogic _bBraunIPLogic;

        #endregion

        void Start()
        {
            _bBraunIPLogic = GetComponent<BBraunIPLogic>();

            // Subscribe to the reactive property
            _bBraunIPLogic.BBraunState.Value.Subscribe(state => {
                // If normal; just ignore whatevs
                if (state != BBraunIPState.NORMAL) 
                {
                    SetAlarmNotif(state);                
                }
            });
        }

        private void SetAlarmNotif(BBraunIPState state)
        {
            switch (state)
            {
                case BBraunIPState.CHECK_UPSTREAM:
                _notifAlarmText.text = "CHECK UPSTREAM";
                break;
                case BBraunIPState.PRESSURE_HIGH:
                _notifAlarmText.text = "PRESSURE HIGH";
                break;
            }
        }
    }
}

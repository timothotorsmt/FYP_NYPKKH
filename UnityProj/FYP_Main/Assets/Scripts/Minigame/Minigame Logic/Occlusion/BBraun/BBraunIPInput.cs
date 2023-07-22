using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BBraunInfusomat
{
    
    public class BBraunIPInput : MonoBehaviour
    {
        #region buttons

        public Button _upButton;
        public Button _downButton;
        public Button _leftButton;
        public Button _rightButton;
        public Button _resetValueButton;
        public Button _initBolusButton;
        public Button _onOffButton;
        public Button _okButton;
        public Button _programButton;
        public Button _startStopInfusionButton;
        public Button _openDoorButton;

        public void RemoveAllFunctionality()
        {
            _upButton.onClick.RemoveAllListeners();
            _downButton.onClick.RemoveAllListeners();
            _leftButton.onClick.RemoveAllListeners();
            _rightButton.onClick.RemoveAllListeners();
            _resetValueButton.onClick.RemoveAllListeners();
            _initBolusButton.onClick.RemoveAllListeners();
            _onOffButton.onClick.RemoveAllListeners();
            _okButton.onClick.RemoveAllListeners();
            _programButton.onClick.RemoveAllListeners();
            _startStopInfusionButton.onClick.RemoveAllListeners();
            _openDoorButton.onClick.RemoveAllListeners();
        }

        #endregion
    } 
}



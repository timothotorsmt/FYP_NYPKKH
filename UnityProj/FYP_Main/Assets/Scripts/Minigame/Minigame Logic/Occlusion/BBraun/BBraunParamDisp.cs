using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BBraunInfusomat
{
    public class BBraunParamDisp : MonoBehaviour
    {
        [SerializeField] private BBraunIPLogic _bBraunIPLogic;
        [SerializeField] private TextMeshProUGUI _vtbiDisplay;
        [SerializeField] private TextMeshProUGUI _timeDisplay;

        // Start is called before the first frame update
        void OnEnable()
        {
            _vtbiDisplay.text = _bBraunIPLogic._idealVTBI.ToString() + " ml";
            _timeDisplay.text = _bBraunIPLogic._idealTime.ToString() + " h : 00 min";
        }
    }
}

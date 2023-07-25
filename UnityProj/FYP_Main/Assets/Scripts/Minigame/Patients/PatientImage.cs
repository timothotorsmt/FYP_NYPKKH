using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using PatientManagement;

[RequireComponent(typeof(Image))]
public class PatientImage : MonoBehaviour
{
    [SerializeField] private PatientList _patients;
    private Image _patientInfo;

    // Start is called before the first frame update
    void Start()
    {
        _patientInfo = GetComponent<Image>();
        _patients.currentPatient.Value.Subscribe(state =>
        {
            if (_patients.currentPatient.GetValue() != null)
            {
                _patientInfo.sprite = _patients.currentPatient.GetValue().bodyModel;
            }
        });
    }
}

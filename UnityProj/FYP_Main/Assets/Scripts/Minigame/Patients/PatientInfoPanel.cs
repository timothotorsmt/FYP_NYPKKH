using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Common.DesignPatterns;
using PatientManagement;

namespace PatientManagement
{
    public class PatientInfoPanel : Singleton<PatientInfoPanel>
    {
        private Patient _patient;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _weight;
        [SerializeField] private TextMeshProUGUI _height;
        [SerializeField] private TextMeshProUGUI _bsa;
        [SerializeField] private TextMeshProUGUI _bmi;

        public void UpdatePatientInfo(Patient newPatient)
        {
            _patient = newPatient;

            _name.text = _patient.name;
            _weight.text = _patient.weight.ToString() + " cm (xx-xxx-20xx)";
            _height.text = _patient.height.ToString() + " kg (xx-xxx-20xx)";
            _bsa.text = _patient.BSA.ToString();
            _bmi.text = _patient.BMI.ToString();
        }
    }
}

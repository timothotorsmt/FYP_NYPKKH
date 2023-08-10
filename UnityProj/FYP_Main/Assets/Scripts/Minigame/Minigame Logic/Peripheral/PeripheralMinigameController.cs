using BBraunInfusomat;
using PatientManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeripheralMinigameController : MonoBehaviour
{
    [SerializeField] private PatientList _patientList;
    [SerializeField] private BBraunIPLogic _bBraunIPLogic;


    // Start is called before the first frame update
    void Start()
    {
        RandomisePatient();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RandomisePatient()
    {
        Patient tempPatient;

        // Generate random patient age group
        int RandNum = Random.Range(0, 2);
        if (false)
        {
            //_patientList.GetRandomPatient(AgeGroup.CHILD);
        }
        else
        {
            tempPatient = _patientList.GetRandomPatient(AgeGroup.ADULT);
        }

        switch (_patientList.currentPatient.GetValue().patientAgeGroup)
        {
            case AgeGroup.CHILD:
                _bBraunIPLogic.SetParamRequirements(12, 500);
                Debug.Log("12, 500");
                break;
            case AgeGroup.ADULT:
                _bBraunIPLogic.SetParamRequirements(6, 500);
                Debug.Log("6, 500");
                break;
        }

        if (PatientInfoPanel.Instance != null)
        {
            PatientInfoPanel.Instance.UpdatePatientInfo(tempPatient);
        }
    }
}

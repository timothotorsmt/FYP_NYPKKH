using PatientManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx.Extention;
using Unity.VisualScripting;
using TMPro;

namespace PatientManagement
{

    [CreateAssetMenu(menuName = "Custom data containers/Patient list")]
    public class PatientList : ScriptableObject
    {
        public List<Patient> _patients = new List<Patient>();

        public ReactiveProp<Patient> currentPatient = new ReactiveProp<Patient>();

        public Patient GetRandomPatient()
        {
            System.Random rand = new System.Random();
            currentPatient.SetValue(_patients[rand.Next(_patients.Count)]);
            return currentPatient.GetValue();
        }

        public Patient GetRandomPatient(AgeGroup specificAgeGroup)
        {
            System.Random rand = new System.Random();
            var patientListSpecificGroup = _patients.Where(x => x.patientAgeGroup == specificAgeGroup).ToList();
            if (patientListSpecificGroup.Count > 0)
            {
                currentPatient.SetValue(patientListSpecificGroup[rand.Next(patientListSpecificGroup.Count)]);
                return currentPatient.GetValue();
            }

            return null;
        }

        public int GetPatientIndex(Patient patientQuery)
        {
            for (int i = 0; i < _patients.Count; i++)
            {
                if (_patients[i] == patientQuery)
                {
                    return i;
                }
            }
            return -1;
        }

        public Patient GetPatientFromIndex(int indexQuery)
        {
            if (indexQuery >= 0 && indexQuery < _patients.Count)
            {
                currentPatient.SetValue(_patients[indexQuery]);
                return _patients[indexQuery];
            }

            return null;
        }
    }
}

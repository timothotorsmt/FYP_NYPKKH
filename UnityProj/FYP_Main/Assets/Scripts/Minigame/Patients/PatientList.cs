using PatientManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx.Extention;

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
    }
}

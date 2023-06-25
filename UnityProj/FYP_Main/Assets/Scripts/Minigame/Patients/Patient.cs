using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PatientManagement
{
    [System.Serializable]
    public class Patient
    {
        // Generic information about the patient
        public string name;
        public string gender;
        [SerializeField, Range(0, 100)] public uint age;

        // health related information
        [SerializeField, Range(0, 250)] public float height;
        [SerializeField, Range(0, 300)] public float weight;

        // Decoration information
        [SerializeField] private GameObject bodyModel;
    }

    // The straights are winning i guess
    public enum gender
    {
        MALE,
        FEMALE
    }
}

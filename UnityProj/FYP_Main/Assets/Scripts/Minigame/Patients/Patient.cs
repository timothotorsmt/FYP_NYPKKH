using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PatientManagement
{
    // This class contains general information of the patient

    [System.Serializable]
    public class Patient
    {
        // Generic information about the patient
        public string name;
        public string gender;
        [SerializeField, Range(0, 100)] public uint age;
        public AgeGroup patientAgeGroup;

        // health related information
        [SerializeField, Range(0, 250)] public float height;
        [SerializeField, Range(0, 300)] public float weight;

        // Decoration information
        [SerializeField] private GameObject bodyModel;

        // Information to be written about the patient
        public List<string> KeyNotes = new List<string>();

        public void AddKeyNotes(string newNote)
        {
            KeyNotes.Add(newNote);
        }
    }

    // The straights are winning i guess
    public enum gender
    {
        MALE,
        FEMALE
    }

    public enum AgeGroup
    {
        TODDLER,
        CHILD,
        ADULT
    }
}

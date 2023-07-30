using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PatientManagement
{
    // This class contains general information of the patient

    [System.Serializable]
    public class Patient
    {
        // Generic information about the patient
        public string name; // Full official name (for display)
        public string altName; // Mr/Ms. person name
        public gender PatientGender;
        public uint age;
        public AgeGroup patientAgeGroup;

        // health related information
        public float height;
        public float weight;

        // Decoration information
        public List<PatientSprites> PatientSpriteList = new List<PatientSprites>();

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

    [System.Serializable]
    public class PatientSprites
    {
        public BodyPart patientBodyPart;
        public Sprite patientSprite;
    }

    public enum BodyPart
    {
        BODY_OVERVIEW,
        ARM,
        STOMA,
        ARM_PHLEBITIS,
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// List of all the different speakers
[CreateAssetMenu(menuName = "Custom data containers/Speaker list")]
public class SpeakerList : ScriptableObject
{
    public List<Speaker> speakerList;
}

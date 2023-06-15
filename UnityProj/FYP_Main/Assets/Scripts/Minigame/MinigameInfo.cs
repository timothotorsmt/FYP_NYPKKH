using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom data containers/Minigame")]
public class MinigameInfo : ScriptableObject
{
    public string minigameName;
    public MinigameID minigameID;
    public GameObject minigamePrefab;
}

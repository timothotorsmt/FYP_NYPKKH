using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Difficulty")]
public class DifficultySettings : ScriptableObject
{
    public Difficulty GameDifficulty;
}

public enum Difficulty
{
    LEVEL_1 = 1, // Easy
    LEVEL_2,
    LEVEL_3,
    LEVEL_4,
    LEVEL_5 = 5, // Normal
    LEVEL_6,
    LEVEL_7,
    LEVEL_8,
    LEVEL_9,
    LEVEL_10 = 10, // Hard
    BOSS
}

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
    EASY = 0,
    NORMAL,
    HARD,
    BOSS
}

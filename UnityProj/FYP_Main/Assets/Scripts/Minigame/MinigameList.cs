using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom data containers/Minigame list")]
public class MinigameList : ScriptableObject
{
    public List<MinigameInfo> minigameList;
}

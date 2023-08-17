using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Is kinda useless tbvh :|
// TODO: remove this from existences
public class MinigameChatGetter : MonoBehaviour
{
    public void DisplayChatLine(string ID)
    {
        if (ID != "") {
            ChatGetter.Instance.StartChat(ID);
        }
    }

    public void DisplayChatLineNoBoss(string ID)
    {
        if (MinigameManager.Instance != null)
        {
            if (MinigameManager.Instance.GetMinigameDifficulty().GameDifficulty != Difficulty.BOSS && MinigameManager.Instance.GetMinigameDifficulty().GameDifficulty != Difficulty.LEVEL_10)
            {
                DisplayChatLine(ID);
            }
        }
    }

    public void GetNothingToDo()
    {
        ChatGetter.Instance.StartChat("#NOTHING");
    }
}

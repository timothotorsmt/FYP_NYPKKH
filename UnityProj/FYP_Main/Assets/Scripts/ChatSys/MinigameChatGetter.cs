using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameChatGetter : MonoBehaviour
{
    public void DisplayChatLine(string ID)
    {
        if (ID != "") {
            ChatGetter.Instance.StartChat(ID);
        }
    }
}

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

    public void GetNothingToDo()
    {
        ChatGetter.Instance.StartChat("#NOTHING");
    }
}

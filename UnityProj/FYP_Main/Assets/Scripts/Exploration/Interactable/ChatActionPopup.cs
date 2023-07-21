using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ChatSys;

public class ChatActionPopup : ChatPopup
{
    public UnityEvent OnChatEnd;

    public override void Interact()
    {
        ChatGetter.Instance.StartChat(_idChat, OnChatEnd);
    }
}

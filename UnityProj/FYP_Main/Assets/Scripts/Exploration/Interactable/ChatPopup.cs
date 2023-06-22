using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChatSys;

public class ChatPopup : Popup
{
    [SerializeField] protected string _idChat;

    public override void Interact()
    {
        ChatGetter.Instance.StartChat(_idChat);
    }
}

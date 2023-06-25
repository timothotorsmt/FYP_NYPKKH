using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChatSys;

public class ChatPopup : Interactable
{
    [SerializeField] protected string _idChat;

    public override void Interact()
    {
        ChatGetter.Instance.StartChat(_idChat);
    }
}

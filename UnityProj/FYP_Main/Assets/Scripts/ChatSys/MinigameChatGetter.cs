using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameChatGetter : MonoBehaviour
{
    private string _defaultText = "#------";
    [SerializeField] private string _level1TextID = "#------";
    [SerializeField] private string _level2TextID = "#------";
    [SerializeField] private string _level3TextID = "#------";
    [SerializeField] private string _bossTextID = "#------";

    public void DisplayChatLine()
    {
        if (_level1TextID != _defaultText) {
            ChatGetter.Instance.StartChat(_level1TextID);
        }
    }
}

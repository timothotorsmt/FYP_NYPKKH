using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An individual chat node
[System.Serializable]
public class ChatNode
{
    // A 6 character code that is used to identify unique chats
    public string ID;
    public uint Order;
    
    // Main body text that is shown
    public string BodyText;

    // Current mood of the chat
    public int Mood;

    // Current Speaker of the statement
    public string Speaker;
    
    // For selection stuff
    // All the different questions
    // First string is the question
    // Second string is the ID of the answer
    public List<List<string>> Questions;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Common.DesignPatterns;
using UnityEngine.Events;

// This class retrieves the chat nodes from the node list
public class ChatGetter : Singleton<ChatGetter>
{
    // All the nodes to be displayed
    private List<ChatNode> _currentNodes;
    // A list of all the questions
    private List<List<string>> _questions;
    // Current index for the chat (order)
    private int _currentIndex;
    // To display the chat UI
    private ChatUIDisplay _chatDisplayUI;
    // The current (or latest) speaker
    private Speaker _speaker;
    // The container containing all the nodes
    [SerializeField] private ChatList _chatListContainer;
    // The container containing all the speakers
    [SerializeField] private SpeakerList _speakerListContainer;
    private UnityEvent _postSpeakingAction;
    
    public void Start()
    {
        _chatDisplayUI = GetComponent<ChatUIDisplay>();
    }

    // Start the chatting system
    public void StartChat(string ID) {
        // Get the nodes, the questions, and the speakers
        _currentNodes = GetChatList(ID);
        _questions = GetQuestions(ID);
        _currentIndex = 0;

        _speaker = GetSpeaker(_currentNodes[_currentIndex].Speaker);

        // Start the chat UI 
        // Display the first node
        _chatDisplayUI.SetChatItem();
        // Fuck this line.
        if (_currentIndex + 1 == _currentNodes.Count && _questions.Count != 0) 
        {
            _chatDisplayUI.SetQuestionItem(_currentNodes[_currentIndex], _speaker, _questions);   
        }
        else 
        {
            _chatDisplayUI.DisplayChatText(_currentNodes[_currentIndex], _speaker);
        }
    }

    public void StartChat(string ID, UnityEvent afterSpeakingAction) {
        StartChat(ID);
        _postSpeakingAction = afterSpeakingAction;
    }

    // Get the next chat node
    public void GetNext() {
        _currentIndex++;
        if (_currentIndex == _currentNodes.Count) {
            // current index is the same as the end of the current node..
            // Stop talking
            _currentIndex = 0;
            _chatDisplayUI.CloseAllChatDisplay();
            if (_postSpeakingAction != null) {
                _postSpeakingAction.Invoke();
            }
            
        }
        else if (_currentIndex + 1 == _currentNodes.Count && _questions.Count != 0) {
            // there exists some questions
            // get the speaker name
            // display all the questions
            _speaker = GetSpeaker(_currentNodes[_currentIndex].Speaker);
            _chatDisplayUI.SetQuestionItem(_currentNodes[_currentIndex], _speaker, _questions);            
        }
        else {
            // Get the node speaker
            // Display the chat node text
            _speaker = GetSpeaker(_currentNodes[_currentIndex].Speaker);
            _chatDisplayUI.DisplayChatText(_currentNodes[_currentIndex], _speaker);
        }
    }

    public List<ChatNode> GetChatList(string check_ID) 
    {
        // Count how many chat nodes with the ID exist
        int count = GetNumChats(check_ID);
        if (count == 0) {
            // if there are no results; display a warning 
            Debug.LogWarning("Requested ID of "+ check_ID + " returned no results. Are you sure the ID is correct?");
            return null;
        }
        
        // get a list of chatnodes, ordered by their order
        var result = _chatListContainer.ChatNodeList.Where(s => s.ID == check_ID).ToList().OrderBy(s => s.Order);
        return result.ToList();
    }

    public Speaker GetSpeaker(string speakerId) 
    {
        // Count how many speakers exist with the speakerID
        int count = _speakerListContainer.speakerList.Where(s => s.SpeakerId.ToString() == speakerId).Count();
        if (count == 0) 
        {
            // if there are no results; display a warning
            Debug.LogWarning("Requested ID of "+ speakerId + " returned no results. Are you sure the ID is correct?");
            return null;
        }

        // Get the first instance of the speaker (if there exists more than 1)
        Speaker result = _speakerListContainer.speakerList.Where(s => s.SpeakerId.ToString() == speakerId).First();
        return result;
    }

    // Get questions for the 
    public List<List<string>> GetQuestions(string check_ID) {
        // Count how many chats with the ID exist
        int count = GetNumChats(check_ID);
        if (count != 0) {
            // This is not really that efficient but we will fix this lat er  
            var result = _chatListContainer.ChatNodeList.Where(s => s.ID == check_ID).ToList().OrderBy(s => s.Order);
            List<List<string>> x = result.Select(s => s.Questions).Last();
            Debug.Log(x);
            return x;
        }
        
        return null;
    }

    private int GetNumChats(string ID)
    {
        return _chatListContainer.ChatNodeList.Count(p => p.ID == ID);
    }

}

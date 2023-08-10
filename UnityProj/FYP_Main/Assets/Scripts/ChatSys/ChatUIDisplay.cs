using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChatUIDisplay : MonoBehaviour
{
    #region UI element variables
    [Tooltip("Overall panel of the chat UI")] [SerializeField] private GameObject _panel;
    [Tooltip("Overall container for the chats")] [SerializeField] private GameObject _chatItem;
    [Tooltip("Overall container for the chat with image")] [SerializeField] private GameObject _chatItemNoImg;
    [Tooltip("Overall container for the chat without character images")] [SerializeField] private GameObject _chatItemWithImg;
    [Tooltip("Overall container for the questions")] [SerializeField] private GameObject _questionItem;
    [Tooltip("[UNUSED] the arrow that indicates that there is a next text")] [SerializeField] private GameObject _nextArrowNormal;
    [Tooltip("[UNUSED] the arrow that indicates that there is a next text")] [SerializeField] private GameObject _nextArrowNoSpeaker;
    [Tooltip("Speaker Name")] [SerializeField] private TextMeshProUGUI _chatSpeaker;
    [Tooltip("Speaker Name but for no image panel")] [SerializeField] private TextMeshProUGUI _chatSpeakerNoImg;
    [Tooltip("Speaker Name but for question panel")] [SerializeField] private TextMeshProUGUI _questionSpeaker;
    [Tooltip("Main chat body text")] [SerializeField] private TextMeshProUGUI _mainBody;
    [Tooltip("Main chat body text for no image")] [SerializeField] private TextMeshProUGUI _mainBodyNoImg;
    [Tooltip("Main chat body text for questions")] [SerializeField] private TextMeshProUGUI _questionMainBody;
    [Tooltip("Character mood sprite")] [SerializeField] private Image _chatSprite;
    [Tooltip("Character mood sprite for questions")] [SerializeField] private Image _questionSprite;
    [Tooltip("Question prefab to spawn")] [SerializeField] private GameObject _questionPrefab;
    [Tooltip("Gameobject for questions to spawn under")] [SerializeField] private GameObject _spawnArea;
    [Tooltip("Question gameobjects")] [SerializeField] private List<GameObject> _questionTag;
    #endregion

    // This functions displays a chat node, given a chat node and speaker
    public void DisplayChatText(ChatNode chatNode, Speaker speaker) {

        if (chatNode.Mood == -1)
        {
            // Set some gameobjects true and false
            // Fuck this project
            _chatItemNoImg.SetActive(true);
            _chatItemWithImg.SetActive(false);

            // Set arrows to active
            //_nextArrowNoSpeaker.SetActive(true);

            _mainBodyNoImg.text = chatNode.BodyText;
            _chatSpeakerNoImg.text = speaker.name;
        }
        else
        {
            _chatItemNoImg.SetActive(false);
            _chatItemWithImg.SetActive(true);

            //_nextArrowNormal.SetActive(true);

            _mainBody.text = chatNode.BodyText;
            // retrieve the mood sprite from the speaker class based on the mood indicated in the CSV file
            if ((int)chatNode.Mood >= speaker.moodImages.Count)
            {
                _chatSprite.sprite = speaker.moodImages[(int)chatNode.Mood];
            }
            else 
            {
              _chatSprite.sprite = speaker.moodImages[0];  
            }
            _chatSpeaker.text = speaker.name;
        }
    }

    public void DisableArrows()
    {
        //_nextArrowNormal.SetActive(false);
        //_nextArrowNoSpeaker.SetActive(false);
    }


    // Activates the chat item and disables the question box
    public void SetChatItem() {
        _panel.SetActive(true);
        _chatItem.SetActive(true);
        _questionItem.SetActive(false);

        if (_questionTag.Count > 0) 
        {
            // Destroy all existing questions (so the previous questions wont be there)
            foreach (GameObject question in _questionTag) {
                Destroy(question);
            }
            _questionTag.Clear();
        }
    }

    // This function sets the question item
    public void SetQuestionItem(ChatNode chatNode, Speaker speaker, List<List<string>> questions) {
        _panel.SetActive(true);
        _chatItem.SetActive(false);
        _questionItem.SetActive(true);

        if (_questionTag.Count > 0) 
        {
            // Destroy all existing questions (so the previous questions wont be there)
            foreach (GameObject question in _questionTag) {
                Destroy(question);
            }
            _questionTag.Clear();
        }

        // Display the question (same as a chat node)
        _questionSpeaker.text = speaker.name;    
        if (chatNode.Mood != -1) 
        {
            _questionSprite.gameObject.SetActive(true);
            _questionSprite.sprite = speaker.moodImages[chatNode.Mood];
        }
        else
        {
            _questionSprite.gameObject.SetActive(false);
        }

        _questionMainBody.text = chatNode.BodyText; 

        // Show all the possible answers
        foreach (List<string> question in questions) {
            // Spawn an answer in the spawn area (has a vertical layout group so it will be positioned nicely)
            GameObject GO = Instantiate(_questionPrefab, _spawnArea.transform);
            
            // Put the answer info inside
            GO.GetComponent<QuestionTag>().SetInfo(question[1], question[0]);
            // Add it to a list of question gameobjects for easy tracking
            _questionTag.Add(GO);
        }
    }
    
    // Close all the chat displays (for chat end)
    public void CloseAllChatDisplay() {
        _chatItem.SetActive(false);
        _questionItem.SetActive(false);
        _panel.SetActive(false);

        if (_questionTag.Count > 0) 
        {
            // Destroy all existing questions (so the previous questions wont be there)
            foreach (GameObject question in _questionTag) {
                Destroy(question);
            }
            _questionTag.Clear();
        }
    }
}

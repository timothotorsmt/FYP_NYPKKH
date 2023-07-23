using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChatUIDisplay : MonoBehaviour
{
    #region UI element variables
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _chatItem;
    [SerializeField] private GameObject _questionItem;
    [SerializeField] private TextMeshProUGUI _chatSpeaker;
    [SerializeField] private TextMeshProUGUI _questionSpeaker;
    [SerializeField] private TextMeshProUGUI _mainBody;
    [SerializeField] private TextMeshProUGUI _questionMainBody;
    [SerializeField] private Image _chatSprite;
    [SerializeField] private Image _questionSprite;
    [SerializeField] private GameObject _questionPrefab;
    [SerializeField] private GameObject _spawnArea;
    [SerializeField] private List<GameObject> _questionTag;
    #endregion

    // This functions displays a chat node, given a chat node and speaker
    public void DisplayChatText(ChatNode chatNode, Speaker speaker) {
        _mainBody.text = chatNode.BodyText;
        
        // retrieve the mood sprite from the speaker class based on the mood indicated in the CSV file
        _chatSprite.sprite = speaker.moodImages[(int)chatNode.Mood];
        _chatSpeaker.text = speaker.name;
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

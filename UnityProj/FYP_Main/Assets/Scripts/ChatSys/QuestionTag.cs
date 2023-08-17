using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This class is for the questions 
public class QuestionTag : MonoBehaviour
{
    // The physical text box to be edited
    [SerializeField] private TextMeshProUGUI questionBlock;
    private string _id;
    private string _text;

    // Click on it
    public void Activate() {
        ChatGetter.Instance.StartChat(_id);
    }

    public void SetInfo(string ID, string Text) {
        _id = ID;
        _text = Text;
        questionBlock.text = _text;
    }
}

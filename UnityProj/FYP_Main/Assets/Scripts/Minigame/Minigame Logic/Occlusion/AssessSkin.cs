using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssessSkin : MonoBehaviour
{
    [SerializeField] private GameObject _button;
    [SerializeField] private MinigameChatGetter _chat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SkinAssess()
    {
        if (OcclusionTaskController.Instance.GetCurrentTask() == OcclusionTasks.ASSESS_SKIN)
        {
            OcclusionTaskController.Instance.MarkCurrentTaskAsDone();
            _chat.DisplayChatLine("#OCCLPA");
            _button.SetActive(false);
        }
        else 
        {
            _chat.DisplayChatLine("#OCCLPC");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Gauze : MonoBehaviour
{
    [SerializeField] private Image _plaster;
    [SerializeField] private GameObject _button;

    // Start is called before the first frame update
    void Start()
    {
        _plaster.color = new Color(1,1,1,0);
    }

    
    public void PutOnPlaster()
    {
        if (OcclusionTaskController.Instance.GetCurrentTask() == OcclusionTasks.PUT_PLASTER)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(_plaster.DOFade(1, 1.0f));
            seq.AppendCallback(() => OcclusionTaskController.Instance.AssignNextTaskContinuous(OcclusionTasks.NUM_MANDATORY_TASKS));
            seq.AppendCallback(() => OcclusionTaskController.Instance.MarkCurrentTaskAsDone());
            _button.SetActive(false);
            
        }
    }
}

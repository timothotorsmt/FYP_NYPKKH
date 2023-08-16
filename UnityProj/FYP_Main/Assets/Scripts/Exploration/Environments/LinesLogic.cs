using Core.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LinesLogic : MonoBehaviour
{
    [SerializeField] private UnityEvent _noBossEvent;
    [SerializeField] private UnityEvent _BossEvent;

    public void GoBackToHub()
    {
        SceneLoader.Instance.ChangeScene(SceneID.HUB_WONDERLAND);
    }

    public void DisplayBossBehavior()
    {
        if (PlayerProgress.Instance != null)
        {
            //if (PlayerProgress.Instance.HasFinishedLines())
            if (true)
            {
                _BossEvent.Invoke();
                return;
            }
        }

        _noBossEvent.Invoke();
    }
}

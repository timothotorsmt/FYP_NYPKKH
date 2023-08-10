using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;
using UniRx;
using UnityEngine.Events;

public class PumpItems : MonoBehaviour
{
    private int _lockedItemCounter;
    [SerializeField] private int _totalNumLock = 3;
    [SerializeField] private UnityEvent _onPumpCloseEvent;

    private void Start()
    {
        _lockedItemCounter = 0;
    }

    public void LockItemIn()
    {
        _lockedItemCounter++;
        if (_lockedItemCounter >= _totalNumLock)
        {
            SetPumpClose();
        }
    }

    public void PlayGreenClipAudio()
    {
        AudioController.Instance.PlayAudio(SoundUID.BBRAUN_DOOR_CLICK_GREEN);
    }

    public void PlayMiddleClipAudio()
    {
        AudioController.Instance.PlayAudio(SoundUID.BBRAUN_DOOR_CLICK_MIDDLE);
    }

    private void SetPumpClose()
    {
        if (PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.ATTACH_TO_PUMP)
        {
            PeripheralSetupTaskController.Instance.MarkCurrentTaskAsDone();
            _onPumpCloseEvent.Invoke();
        }
    }
}

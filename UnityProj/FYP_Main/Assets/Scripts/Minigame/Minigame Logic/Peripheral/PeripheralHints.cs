using UnityEngine;
using System.Collections;
using UniRx;
using MinigameBase;

public class PeripheralHints : HintSystemBase
{
    void Start()
    {
        // used as kinda of an event system
        PeripheralSetupTaskController.Instance.CurrentTask.Value.Subscribe(_ => {
            if (_isRunningHint)
            {
                StopCoroutine(HintCounter());
            }
            StartCoroutine(HintCounter());
        });
    }

    public void GetHint()
    {
        _button.SetActive(false);
        switch (PeripheralSetupTaskController.Instance.GetCurrentTask())
        {
            case PeripheralSetupTasks.PEEL_OFF_FOIL:
                _chatGetter.DisplayChatLine("#PERIHA");
                break;
            case PeripheralSetupTasks.CLEAN_WITH_SWAB:
                _chatGetter.DisplayChatLine("#PERIHB");
                break;
            case PeripheralSetupTasks.CLAMP_ROLLER_CLAMP:
                _chatGetter.DisplayChatLine("#PERIHC");
                break;
            case PeripheralSetupTasks.REMOVE_PROTECTIVE_CAP:
                _chatGetter.DisplayChatLine("#PERIHD");
                break;
            case PeripheralSetupTasks.SPIKE_INFUSION_BOTTLE:
                _chatGetter.DisplayChatLine("#PERIHE");
                break;
            case PeripheralSetupTasks.PRIME_INFUSION_TUBING:
                _chatGetter.DisplayChatLine("#PERIHF");
                break;
            case PeripheralSetupTasks.OPEN_ROLLER_CLAMP:
                _chatGetter.DisplayChatLine("#PERIHG");
                break;
            case PeripheralSetupTasks.OPEN_DOOR:
                _chatGetter.DisplayChatLine("#PERIHH");
                break;
            case PeripheralSetupTasks.ATTACH_TO_PUMP:
                _chatGetter.DisplayChatLine("#PERIHI");
                break;
            case PeripheralSetupTasks.CLEAN_IV_CLAVE:
                _chatGetter.DisplayChatLine("#PERIHK");
                break;
            case PeripheralSetupTasks.CONNECT_INFUSION_TUBING:
                _chatGetter.DisplayChatLine("#PERIHL");
                break;
            case PeripheralSetupTasks.START_PUMP:
                _chatGetter.DisplayChatLine("#PERIHM");
                break;
        }
    }
}



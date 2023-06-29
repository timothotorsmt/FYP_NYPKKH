using BBraunInfusomat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionMinigameController : MonoBehaviour
{
    [SerializeField] private DifficultySettings _minigameDifficultySettings;
    [SerializeField] private BBraunIPLogic _bBraunIPLogic;
    [SerializeField] private OcclusionRollerClamp _rollerClamp;

    private OcclusionScenario _occlusionScenario;

    // Start is called before the first frame update
    void Start()
    {
        RandomiseAlarm();
    }

    private void RandomiseAlarm()
    {
        switch (_minigameDifficultySettings.GameDifficulty)
        {
            case Difficulty.LEVEL_1:
                // Hardcode the alarm code 
                _occlusionScenario = OcclusionScenario.CLAMPED_ROLLER_CLAMP;
                _bBraunIPLogic.SetBBraunAlarm(BBraunIPState.CHECK_UPSTREAM);
                _rollerClamp.SetRollerClampClose();
                OcclusionTaskController.Instance.AssignCurrentTaskContinuous(OcclusionTasks.OPEN_ROLLER_CLAMP);
                OcclusionTaskController.Instance.AssignNextTaskContinuous(OcclusionTasks.MUTE_ALARM);
                break;
            case Difficulty.LEVEL_2:
                // Hardcode the alarm code 
                _occlusionScenario = OcclusionScenario.KINKED_LINES;
                _bBraunIPLogic.SetBBraunAlarm(BBraunIPState.PRESSURE_HIGH);
                OcclusionTaskController.Instance.AssignCurrentTaskContinuous(OcclusionTasks.UNKINK_LINE);
                Debug.Log(OcclusionTaskController.Instance.GetCurrentTask().ToString());
                break;
            case Difficulty.LEVEL_3:
                // Hardcode the alarm code 
                _occlusionScenario = OcclusionScenario.T_CONNECTOR;
                _bBraunIPLogic.SetBBraunAlarm(BBraunIPState.PRESSURE_HIGH);
                OcclusionTaskController.Instance.AssignCurrentTaskContinuous(OcclusionTasks.UNCLAMP_T_CONNECTOR);
                break;
            case Difficulty.LEVEL_4:
            case Difficulty.LEVEL_5:
                // Hardcode the alarm code 
                _occlusionScenario = OcclusionScenario.PHLEBITIS;
                _bBraunIPLogic.SetBBraunAlarm(BBraunIPState.PRESSURE_HIGH);
                OcclusionTaskController.Instance.AssignCurrentTaskContinuous(OcclusionTasks.CLAMP_T_CONNECTOR);
                break;
            case Difficulty.LEVEL_10:
            case Difficulty.BOSS:
                break;
        }
    }
}

public enum OcclusionScenario
{
    CLAMPED_ROLLER_CLAMP,
    KINKED_LINES,
    T_CONNECTOR,
    PHLEBITIS
}

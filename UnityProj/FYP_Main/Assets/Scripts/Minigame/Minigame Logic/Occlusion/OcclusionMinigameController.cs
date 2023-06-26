using BBraunInfusomat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionMinigameController : MonoBehaviour
{
    [SerializeField] private DifficultySettings _minigameDifficultySettings;
    [SerializeField] private BBraunIPLogic _bBraunIPLogic;

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
                OcclusionTaskController.Instance.AssignNextTaskContinuous(OcclusionTasks.OPEN_ROLLER_CLAMP);
                break;
            case Difficulty.LEVEL_2:
                // Hardcode the alarm code 
                _occlusionScenario = OcclusionScenario.KINKED_LINES;
                _bBraunIPLogic.SetBBraunAlarm(BBraunIPState.PRESSURE_HIGH);
                OcclusionTaskController.Instance.AssignNextTaskContinuous(OcclusionTasks.UNKINK_LINE);
                break;
            case Difficulty.LEVEL_3:
                // Hardcode the alarm code 
                _occlusionScenario = OcclusionScenario.T_CONNECTOR;
                _bBraunIPLogic.SetBBraunAlarm(BBraunIPState.PRESSURE_HIGH);
                OcclusionTaskController.Instance.AssignNextTaskContinuous(OcclusionTasks.UNCLAMP_T_CONNECTOR);
                break;
            case Difficulty.LEVEL_4:
            case Difficulty.LEVEL_5:
                // Hardcode the alarm code 
                _occlusionScenario = OcclusionScenario.PHLEBITIS;
                _bBraunIPLogic.SetBBraunAlarm(BBraunIPState.PRESSURE_HIGH);
                OcclusionTaskController.Instance.AssignNextTaskContinuous(OcclusionTasks.CLAMP_T_CONNECTOR);
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

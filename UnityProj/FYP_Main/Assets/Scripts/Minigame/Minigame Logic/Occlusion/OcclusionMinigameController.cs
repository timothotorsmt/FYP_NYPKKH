using BBraunInfusomat;
using PatientManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionMinigameController : MonoBehaviour
{
    [SerializeField] private DifficultySettings _minigameDifficultySettings;
    [SerializeField] private BBraunIPLogic _bBraunIPLogic;
    [SerializeField] private OcclusionRollerClamp _rollerClamp;
    [SerializeField] private TConnector _tConnector;
    [SerializeField] private Phlebitis _phlebitisController;
    [SerializeField] private PatientList _patientList;

    private OcclusionScenario _occlusionScenario;
    private Patient _currentPatient;

    // Start is called before the first frame update
    void Start()
    {
        RandomisePatient();
        RandomiseAlarm();
    }

    public OcclusionScenario GetOcclusionScenario()
    {
        return _occlusionScenario;
    }

    private void RandomiseAlarm()
    {
        switch (_patientList.currentPatient.GetValue().patientAgeGroup)
        {
            case AgeGroup.CHILD:
                _bBraunIPLogic.SetParams(41.7f, 12, 500);
                break;
            case AgeGroup.ADULT:
                _bBraunIPLogic.SetParams(83.3f, 6, 500);
                break;
        }

        switch (_minigameDifficultySettings.GameDifficulty)
        {
            case Difficulty.LEVEL_1:
                // Hardcode the alarm code 
                _occlusionScenario = OcclusionScenario.CLAMPED_ROLLER_CLAMP;
                _bBraunIPLogic.SetBBraunAlarm(BBraunIPState.CHECK_UPSTREAM);
                _rollerClamp.SetRollerClampClose();
                OcclusionTaskController.Instance.AssignCurrentTaskContinuous(OcclusionTasks.MUTE_ALARM);
                OcclusionTaskController.Instance.AssignNextTaskContinuous(OcclusionTasks.OPEN_ROLLER_CLAMP);
                break;
            case Difficulty.LEVEL_2:
                // Hardcode the alarm code 
                _occlusionScenario = OcclusionScenario.KINKED_LINES;
                _bBraunIPLogic.SetBBraunAlarm(BBraunIPState.PRESSURE_HIGH);
                OcclusionTaskController.Instance.AssignCurrentTaskContinuous(OcclusionTasks.MUTE_ALARM);
                OcclusionTaskController.Instance.AssignNextTaskContinuous(OcclusionTasks.UNKINK_LINE);
                break;
            case Difficulty.LEVEL_3:
                // Hardcode the alarm code 
                _occlusionScenario = OcclusionScenario.T_CONNECTOR;
                _bBraunIPLogic.SetBBraunAlarm(BBraunIPState.PRESSURE_HIGH);
                _tConnector.CloseTConnector();
                OcclusionTaskController.Instance.AssignCurrentTaskContinuous(OcclusionTasks.MUTE_ALARM);
                OcclusionTaskController.Instance.AssignNextTaskContinuous(OcclusionTasks.UNCLAMP_T_CONNECTOR);
                break;
            case Difficulty.LEVEL_4:
                // Hardcode the alarm code 
                _occlusionScenario = OcclusionScenario.THREEWAY_TAP;
                _bBraunIPLogic.SetBBraunAlarm(BBraunIPState.PRESSURE_HIGH);
                OcclusionTaskController.Instance.AssignCurrentTaskContinuous(OcclusionTasks.MUTE_ALARM);
                OcclusionTaskController.Instance.AssignNextTaskContinuous(OcclusionTasks.UNCLAMP_T_CONNECTOR);
                break;
            case Difficulty.LEVEL_5:
            case Difficulty.LEVEL_6:
                // Hardcode the alarm code 
                _occlusionScenario = OcclusionScenario.PHLEBITIS;
                _bBraunIPLogic.SetBBraunAlarm(BBraunIPState.PRESSURE_HIGH);

                _phlebitisController.SetPhlebitis();
                OcclusionTaskController.Instance.AssignCurrentTaskContinuous(OcclusionTasks.MUTE_ALARM);
                OcclusionTaskController.Instance.AssignNextTaskContinuous(OcclusionTasks.ASSESS_SKIN);
                break;
            case Difficulty.LEVEL_10:
                // Basically boss but everything else
                int RandNum = Random.Range(0, 3);
                if (RandNum == 0)
                {
                    // Hardcode the alarm code 
                    _occlusionScenario = OcclusionScenario.T_CONNECTOR;
                    _bBraunIPLogic.SetBBraunAlarm(BBraunIPState.PRESSURE_HIGH);
                    _tConnector.CloseTConnector();
                    OcclusionTaskController.Instance.AssignCurrentTaskContinuous(OcclusionTasks.MUTE_ALARM);
                    OcclusionTaskController.Instance.AssignNextTaskContinuous(OcclusionTasks.UNCLAMP_T_CONNECTOR);
                }
                else if (RandNum == 1)
                {
                    // Hardcode the alarm code 
                    _occlusionScenario = OcclusionScenario.KINKED_LINES;
                    _bBraunIPLogic.SetBBraunAlarm(BBraunIPState.PRESSURE_HIGH);
                    OcclusionTaskController.Instance.AssignCurrentTaskContinuous(OcclusionTasks.MUTE_ALARM);
                    OcclusionTaskController.Instance.AssignNextTaskContinuous(OcclusionTasks.UNKINK_LINE);
                }
                else
                {
                    // Hardcode the alarm code 
                    _occlusionScenario = OcclusionScenario.CLAMPED_ROLLER_CLAMP;
                    _bBraunIPLogic.SetBBraunAlarm(BBraunIPState.CHECK_UPSTREAM);
                    _rollerClamp.SetRollerClampClose();
                    OcclusionTaskController.Instance.AssignCurrentTaskContinuous(OcclusionTasks.MUTE_ALARM);
                    OcclusionTaskController.Instance.AssignNextTaskContinuous(OcclusionTasks.OPEN_ROLLER_CLAMP);
                }
                break;
            case Difficulty.BOSS:
                // Boss but only phlebitis
                // Hardcode the alarm code 
                _occlusionScenario = OcclusionScenario.PHLEBITIS;
                _bBraunIPLogic.SetBBraunAlarm(BBraunIPState.PRESSURE_HIGH);

                _phlebitisController.SetPhlebitis();
                OcclusionTaskController.Instance.AssignCurrentTaskContinuous(OcclusionTasks.MUTE_ALARM);
                OcclusionTaskController.Instance.AssignNextTaskContinuous(OcclusionTasks.ASSESS_SKIN);
                break;
        }
    }

    private void RandomisePatient()
    {
        Patient tempPatient;

        // Generate random patient age group
        int RandNum = Random.Range(0, 2);
        if (RandNum == 0)
        {
            tempPatient = _patientList.GetRandomPatient(AgeGroup.TODDLER);

        }
        else
        {
            tempPatient = _patientList.GetRandomPatient(AgeGroup.TODDLER);
        }

        if (PatientInfoPanel.Instance != null)
        {
            PatientInfoPanel.Instance.UpdatePatientInfo(tempPatient);
        }
    }
}

public enum OcclusionScenario
{
    CLAMPED_ROLLER_CLAMP,
    KINKED_LINES,
    T_CONNECTOR,
    THREEWAY_TAP,
    PHLEBITIS
}

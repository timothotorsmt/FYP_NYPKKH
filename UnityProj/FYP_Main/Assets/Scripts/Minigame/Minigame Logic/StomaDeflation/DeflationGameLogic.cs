using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Extention;
using UniRx;
using UnityEngine.Events;

public class DeflationGameLogic : BasicSlider
{
    [SerializeField] private DifficultySettings _minigameDifficulty;
    [SerializeField, Min(0f)] private float _gameLength = 180;
    [SerializeField] private GameObject _nextButton;
    [SerializeField] private GameObject _prevButton;

    [SerializeField] private ClipBag _clipBag;
    [SerializeField] private UnclipBag _unclipBag;
    [SerializeField] private RollBag _rollBag;
    [SerializeField] private UnrollBag _unrollBag;

    private bool _isGameRunning;

    private int _lives=6;
    public UnityEvent GameLost;
    public UnityEvent GameWin;

    private static int _numPatients = 1;
    private ReactiveProp<int> _patientCounter;

    private List<StomaPatient> _patients;

    // Start is called before the first frame update
    void Start()
    {
        _isGameRunning = true;
        _patientCounter = new ReactiveProp<int>();
        _patients = new List<StomaPatient>();

        // Set num patients based on difficulty level
        switch (_minigameDifficulty.GameDifficulty)
        {
            case Difficulty.BOSS:
                _numPatients = 1;
                break;
            default:
                _numPatients = 1;
                break;
        }

        // Spawn assets based on num of patients
        for (int i = 0; i < _numPatients; i++)
        {
            StomaPatient tempPatient = new StomaPatient();
            tempPatient.StomaBagAirValue.SetValue(1.0f);
            //StartCoroutine(StomaBagBehaviour(tempPatient));
            _patients.Add(tempPatient);
        }

        _patientCounter.Value.Subscribe(state =>
        {
            if (state == 0)
            {
                _prevButton.SetActive(false);
            }
            else
            {
                _prevButton.SetActive(true);
            }

            if (state == _numPatients - 1)
            {
                _nextButton.SetActive(false);
            }
            else
            {
                _nextButton.SetActive(true);
            }
        });

        _patientCounter.SetValue(0);

        // Create patient information

        // Start game timer
        StartCoroutine(GameLength());
    }

    private IEnumerator GameLength()
    {
        yield return new WaitForSeconds(_gameLength);
        FinsihGameLose();
        ChatGetter.Instance.StartChat("#STOMAD", GameWin);
    
    }


    public void RemoveLife()
    {
        _lives -=1;
        DeflationTaskController.Instance.MarkWrongTask();

        if (_lives==0)
        {
            ChatGetter.Instance.StartChat("#STOMAE", GameLost);
            _isGameRunning = false;
            StopCoroutine("GameLength");
        }
    }

    public bool getGamerunning()
    {
        //is it ok to put this what is this for
        return _isGameRunning;
    }
    
    public void FinsihGameLose()
    {
        _lives = -1;
        DeflationTaskController.Instance.MarkCorrectTask(false);
        CheckIfGameOver();
    }
    public void FinsihGameWin()
    {
        Debug.Log("asdd");
        DeflationTaskController.Instance.MarkCorrectTask();
        _isGameRunning = false;
        CheckIfGameOver();
    }

    private void CheckIfGameOver()
    {
        if (!_isGameRunning && DeflationTaskController.Instance.GetCurrentTask() == DeflationTasks.DEFLATE_BAGS)
        {
            Debug.Log("powerk");
            DeflationTaskController.Instance.MarkCurrentTaskAsDone();
        }
        if(!_isGameRunning)
        {
            Debug.Log("asdasd");
            DeflationTaskController.Instance.MarkCurrentTaskAsDone();
        }
    }

    private IEnumerator StomaBagBehaviour(StomaPatient currPatient)
    {
        yield return new WaitForSeconds(currPatient.StomaBagAirFillDelay);

        currPatient.AddStomaBagValue();

        
        currPatient.StomaBagAirValue.SetValue(Mathf.Min(1, currPatient.StomaBagAirValue.GetValue()));


        StartCoroutine(StomaBagBehaviour(currPatient));
    }

    public void ResetMinigame()
    {
        _clipBag.Reset();
        _unclipBag.Reset();
        _rollBag.Reset();
        _unrollBag.Reset();

        CheckIfGameOver();
    }

    public int GetNumPatients()
    {
        return _numPatients;
    }

    public StomaPatient GetCurrentPatient()
    {
        return _patients[_patientCounter.GetValue()];
    }

    public void GetNextPatient()
    {
        _patientCounter.SetValue(_patientCounter.GetValue() + 1);
    }

    public void GetPrevPatient()
    {
        _patientCounter.SetValue(_patientCounter.GetValue() - 1);
    }    
}

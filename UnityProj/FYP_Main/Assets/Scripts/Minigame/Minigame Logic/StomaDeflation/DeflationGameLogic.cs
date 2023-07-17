using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Extention;
using UniRx;
using UnityEngine.Events;

public class DeflationGameLogic : BasicSlider
{
    [SerializeField] private DifficultySettings _minigameDifficulty;
    [SerializeField, Min(0)] private float _playTimer;
    [SerializeField] private GameObject _nextButton;
    [SerializeField] private GameObject _prevButton;

    [SerializeField] private ClipBag _clipBag;
    [SerializeField] private UnclipBag _unclipBag;
    [SerializeField] private RollBag _rollBag;
    [SerializeField] private UnrollBag _unrollBag;

    private static int _numPatients = 1;
    private ReactiveProp<int> _patientCounter;

    private List<StomaPatient> _patients;

    // Start is called before the first frame update
    void Start()
    {
        _patientCounter = new ReactiveProp<int>();
        _patients = new List<StomaPatient>();

        // Set num patients based on difficulty level
        switch (_minigameDifficulty.GameDifficulty)
        {
            case Difficulty.BOSS:
                _numPatients = 1;
                break;
            default:
                _numPatients = 3;
                break;
        }

        // Spawn assets based on num of patients
        for (int i = 0; i < _numPatients; i++)
        {
            StomaPatient tempPatient = new StomaPatient();
            StartCoroutine(StomaBagBehaviour(tempPatient));
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
    }

    private IEnumerator StomaBagBehaviour(StomaPatient currPatient)
    {
        yield return new WaitForSeconds(currPatient.StomaBagAirFillDelay);

        currPatient.AddStomaBagValue();

        // If its past the max value, then set back d max value
        if (currPatient.StomaBagAirValue.GetValue() > 1)
        {
            // Display fail case
            Debug.Log("HI");
        }
        currPatient.StomaBagAirValue.SetValue(Mathf.Min(1, currPatient.StomaBagAirValue.GetValue()));


        StartCoroutine(StomaBagBehaviour(currPatient));
    }

    public void ResetMinigame()
    {
        _clipBag.Reset();
        _unclipBag.Reset();
        _rollBag.Reset();
        _unrollBag.Reset();
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

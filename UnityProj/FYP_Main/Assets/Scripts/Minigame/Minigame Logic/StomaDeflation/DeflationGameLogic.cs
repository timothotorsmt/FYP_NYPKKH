using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Extention;
using UniRx;

public class DeflationGameLogic : MonoBehaviour
{
    [SerializeField] private DifficultySettings _minigameDifficulty;
    [SerializeField] private GameObject _nextButton;
    [SerializeField] private GameObject _prevButton;
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

        _patientCounter.Value.Subscribe(state => {
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
        StartCoroutine(StomaBagBehaviour(currPatient));
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigamePerformance : MonoBehaviour
{
    private int _totalNumPoints = 0; // The total number of points the player has at the moment 
    private int _totalPossiblePoints = 0; // The total number of possible points that can be earned in the minigame
    private List<string> _errors;
    public Grade PerformanceGrade;
    private int _pointsToAdd;
    private int _pointsToNextRank;

    // UI variables
    [SerializeField] private GameObject _performanceReviewScreen;
    [SerializeField] private GameObject _titleScreen;
    [SerializeField] private Image _resultDisplay;
    [SerializeField] private GameObject _levelUp;
    [SerializeField] private Slider _levelUpSlider;

    // Start is called before the first frame update
    void Start()
    {
        _errors = new List<string>();
    }

    public void AddPositiveAction()
    {
        _totalNumPoints += 10;
        _totalPossiblePoints += 10;
    }

    public void AddNegativeAction(string error)
    {
        _totalNumPoints -= 10;
        _errors.Add(error);
    }

    public void EvaluatePerformance()
    {
        float score = _totalNumPoints / _totalPossiblePoints;
        if (score >= 0.75f)
        {
            PerformanceGrade = Grade.PERFECT;
        }
        else if (score >= 0.5f)
        {
            PerformanceGrade = Grade.GREAT;
        }
        else if (score >= 0.25f)
        {
            PerformanceGrade = Grade.GOOD;
        }
        else if (score >= -0.25)
        {
            PerformanceGrade = Grade.MEH;
        }
        else {
            PerformanceGrade = Grade.FAIL;
        }

        _performanceReviewScreen.SetActive(true);
    }
}

// Change to this instead because grade may be too harsh
public enum Grade
{
    PERFECT,
    GREAT,
    GOOD,
    MEH,
    FAIL
}

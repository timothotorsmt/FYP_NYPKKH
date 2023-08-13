using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Input;
using Common.DesignPatterns;

public class MinigamePerformance : Singleton<MinigamePerformance>
{
    #region variables

    private float _totalNumPoints = 0; // The total number of points the player has at the moment 
    private float _totalPossiblePoints = 0; // The total number of possible points that can be earned in the minigame
    public Grade PerformanceGrade;

    // For the ranking thing the nurses want
    // TODO: next group pls add the ranking thing :||||||
    private static int _pointsToAdd;
    private int _pointsToNextRank;
    
    [SerializeField] private MinigameReaction _reaction;
    private MinigamePerformanceUI _minigamePerformanceUI;   

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _minigamePerformanceUI = this.GetComponent<MinigamePerformanceUI>();
    }

    public void AddPositiveAction(bool PlayParticleEffect = true)
    {
        _totalNumPoints += 10;
        _totalPossiblePoints += 10;
        if (PlayParticleEffect)
        {
            _reaction.SetHappyReaction(InputUtils.GetInputPosition());
        }
    }

    public void AddNegativeAction(bool PlayParticleEffect = true)
    {
        _totalNumPoints -= 15;
        if (PlayParticleEffect)
        {
            _reaction.SetSadReaction(InputUtils.GetInputPosition());
        }
    }

    public void EvaluatePerformance()
    {
        float score = _totalNumPoints / _totalPossiblePoints;
        Debug.Log(score);
        if (score >= 0.9f)
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

        if (!PlayerProgress.Instance)
        {
            PlayerProgress.Instance.AddMinigameScore(MinigameManager.Instance.GetCurrentMinigame().minigameID, PerformanceGrade);
        }

        _minigamePerformanceUI.AssignResult(PerformanceGrade);
        _minigamePerformanceUI.DisplayResult();
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

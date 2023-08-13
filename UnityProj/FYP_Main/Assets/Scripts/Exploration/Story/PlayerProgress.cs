using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.DesignPatterns;
using System.Linq;

// Keeps track of the player progress and everything
public class PlayerProgress : SingletonPersistent<PlayerProgress>
{
    [System.Serializable]
    public struct MinigameScore 
    {
        public MinigameID MinigameID;
        public Grade MinigameGrade;

        /// <summary>
        /// CTOR for minigame score
        /// </summary>
        /// <param name="minigameID"> Minigame ID to be added </param>
        /// <param name="grade"> Grade of said minigame </param>
        public MinigameScore(MinigameID minigameID, Grade grade) 
        {
            this.MinigameID = minigameID;
            this.MinigameGrade = grade;
        }
    }

    // temporarily save the player code
    public List<MinigameScore> _playerMinigameScores = new List<MinigameScore>();

    public void AddMinigameScore(MinigameID newMinigameID, Grade newGrade)
    {
        if (_playerMinigameScores.Where(x => x.MinigameID == newMinigameID).Count() > 0)
        {
           _playerMinigameScores.Remove(_playerMinigameScores.Where(x => x.MinigameID == newMinigameID).First());
        }
        
        MinigameScore newScore = new MinigameScore(newMinigameID, newGrade);
        _playerMinigameScores.Append(newScore);
    }

    /// <summary>
    /// Check if the player is eligible for the boss section
    /// </summary>
    /// <returns> returns whether the player is eligible for the boss section </returns>
    public bool HasFinishedLines()
    {
        if (_playerMinigameScores.Where(x => x.MinigameID == MinigameID.OCCLUSION_1).Count() != 0 && 
            _playerMinigameScores.Where(x => x.MinigameID == MinigameID.PERIPHERAL_SETUP).Count() != 0 &&
            _playerMinigameScores.Where(x => x.MinigameID == MinigameID.CVL_PREREQUISITE).Count() != 0)
        {
            if (_playerMinigameScores.Where(x => x.MinigameID == MinigameID.OCCLUSION_1).First().MinigameGrade != Grade.FAIL && 
                _playerMinigameScores.Where(x => x.MinigameID == MinigameID.PERIPHERAL_SETUP).First().MinigameGrade != Grade.FAIL &&
                _playerMinigameScores.Where(x => x.MinigameID == MinigameID.CVL_PREREQUISITE).First().MinigameGrade != Grade.FAIL)
            {
                return true;
            }

        }

        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.DesignPatterns;
using Core.SceneManagement;
using System.Linq;

public class MinigameManager : SingletonPersistent<MinigameManager>
{
    private MinigameInfo _currentMinigame;
    [SerializeField] private DifficultySettings _difficulty;
    [SerializeField] private MinigameList _minigameList;
    private SceneID _hubSceneID;

    // Start the minigame
    public void StartMinigame(MinigameID minigameID, Difficulty _gameDifficulty = Difficulty.LEVEL_1) 
    {
        if (SceneLoader.Instance.GetSceneID() != SceneID.MINIGAME)
        {
            _hubSceneID = SceneLoader.Instance.GetSceneID();
        }
        
        _currentMinigame = getMinigameInfo(minigameID);
        // Set the difficulty of the current game
        _difficulty.GameDifficulty = _gameDifficulty;

        // Once all info is set, change game to the minigame scene :)
        SceneLoader.Instance.ChangeScene(SceneID.MINIGAME, true);
    }

    // Start the minigame
    public void StartBossMinigame(MinigameID minigameID)
    {
        _currentMinigame = getMinigameInfo(minigameID);
        // Set the difficulty of the current game
        _difficulty.GameDifficulty = Difficulty.BOSS;

        // Once all info is set, change game to the minigame scene :)
        SceneLoader.Instance.ChangeScene(SceneID.BOSS, true);
    }

    public MinigameInfo GetCurrentMinigame()
    {
        return _currentMinigame;
    }

    public DifficultySettings GetMinigameDifficulty()
    {
        return _difficulty;
    }

    public SceneID GetHubID()
    {
        return _hubSceneID;
    }

    public MinigameInfo getMinigameInfo(MinigameID minigameID) 
    {
        // Get count of how many minigames exist with this ID
        if (_minigameList.minigameList.Where(s => s.minigameID == minigameID).Count() == 0)
        {
            // Show error message
            Debug.LogWarning("Unable to find any minigames with ID:" + minigameID + " could be found.");
            return null;
        }

        MinigameInfo returnVar = _minigameList.minigameList.Where(s => s.minigameID == minigameID).First();
        return returnVar;
    }
}

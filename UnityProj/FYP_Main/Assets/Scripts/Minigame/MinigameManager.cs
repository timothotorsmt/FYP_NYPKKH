using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.DesignPatterns;
using Core.SceneManagement;
using System.Linq;

public class MinigameManager : SingletonPersistent<MinigameManager>
{
    private MinigameInfo _currentMinigame;
    [SerializeField] private MinigameList _minigameList;

    // TODO: make this follow a settings page
    public void StartMinigame(MinigameID minigameID) 
    {
        _currentMinigame = getMinigameInfo(minigameID);
        SceneLoader.Instance.ChangeScene(SceneID.MINIGAME);
    }

    public MinigameInfo GetCurrentMinigame()
    {
        return _currentMinigame;
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

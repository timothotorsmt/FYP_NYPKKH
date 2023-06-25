using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.SceneManagement;
using Common.DesignPatterns;

public class MinigameSceneController : Singleton<MinigameSceneController>
{
    [SerializeField] private GameObject _spawnPoint;
    private GameObject _currentMinigameObject;

    // Start is called before the first frame update
    void Start()
    {
        MinigameInfo currentMinigame = null;
        if (MinigameManager.Instance != null) {
            currentMinigame = MinigameManager.Instance.GetCurrentMinigame();
        } 
        else 
        {
            Debug.LogWarning("Minigame manager does not have a mingame parameter at the moment :| Be scared if you're testing d whole game otherwise ignore");
        }

        // safety first!
        if (currentMinigame != null) {
            _currentMinigameObject = Instantiate(currentMinigame.minigamePrefab, _spawnPoint.transform);
        }
    }

    public void EndMinigame()
    {
        // Update points and whatever here ig
        // Save game here too for good measure

        GoBackToLevel();
    }

    private void GoBackToLevel()
    {
        // Change scene
        SceneLoader.Instance.ChangeScene(SceneID.HUB);
    }
}

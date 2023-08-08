using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.SceneManagement;
using Common.DesignPatterns;
using UnityEngine.Events;

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
            _currentMinigameObject.SetActive(true);
        }
    }

    public void EndMinigame()
    {
        GoBackToLevel();
        LinesBossLogic.Instance.FinishMinigame();
    }

    private void GoBackToLevel()
    {
        // Change scene
        SceneLoader.Instance.ChangeScene(MinigameManager.Instance.GetHubID(), true);
        
    }

    public void GoBackToHub()
    {
        SceneLoader.Instance.ChangeScene(SceneID.HUB_WONDERLAND);

    }
}

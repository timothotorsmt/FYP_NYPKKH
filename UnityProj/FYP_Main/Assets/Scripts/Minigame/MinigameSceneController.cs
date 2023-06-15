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
        MinigameInfo currentMinigame = MinigameManager.Instance.GetCurrentMinigame();

        // safety first!
        if (currentMinigame != null) {
            _currentMinigameObject = Instantiate(currentMinigame.minigamePrefab, _spawnPoint.transform);
        }
    }

    public void EndMinigame()
    {
        // Update points and whatever here ig
        // Save game here too for good measure

        // Change scene
        SceneLoader.Instance.ChangeScene(SceneID.HUB);
    }
}

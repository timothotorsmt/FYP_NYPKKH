using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayNewGame()
    {
        // Change scene
        SceneLoader.Instance.ChangeScene(SceneID.HUB_WONDERLAND, true);

        if (PlayerProgress.Instance != null)
        {
            // Delete all player progress byeeee
            Destroy(PlayerProgress.Instance.gameObject);
        }
    }

    public void Continue()
    {
        // Change scene
        SceneLoader.Instance.ChangeScene(SceneID.HUB_WONDERLAND, true);
    }

    public void LoadGame()
    {
        // Check if file exists and if not use play game
        PlayerDataSaver.SetLoadedData();
        // Set the saved scene and postion
        SceneLoader.Instance.ChangeScene(PlayerDataSaver.playerData.currentSceneID, true);
        //PlayerManager.Instance.GetPlayerMovement().gameObject.transform.position = PlayerDataSaver.playerData.playerPos;
        PlayerProgress.Instance.hasDoneStory = PlayerDataSaver.playerData.hasDoneStory;
    }

    public void GoToSettings()
    {
        // Change scene
        SceneLoader.Instance.ChangeScene(SceneID.SETTINGS);
    }
}


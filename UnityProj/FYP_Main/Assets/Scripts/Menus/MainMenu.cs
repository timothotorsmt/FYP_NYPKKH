using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Change scene
        SceneLoader.Instance.ChangeScene(SceneID.HUB_NORMAL, true);
    }

    public void LoadGame()
    {
        // Check if file exists and if not use play game
        PlayerDataSaver.SetLoadedData();
        // Set the saved scene and postion
        SceneLoader.Instance.ChangeScene(PlayerDataSaver.playerData.currentSceneID, true);

    }

    public void GoToSettings()
    {
        // Change scene
        SceneLoader.Instance.ChangeScene(SceneID.SETTINGS);
    }
}

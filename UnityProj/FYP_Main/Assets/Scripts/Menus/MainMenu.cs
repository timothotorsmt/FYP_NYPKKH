using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Change scene
        SceneLoader.Instance.ChangeScene(SceneID.HUB);
    }

    public void GoToSettings()
    {
        // Change scene
        SceneLoader.Instance.ChangeScene(SceneID.SETTINGS);
    }
}

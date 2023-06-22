using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.SceneManagement;

public class SettingsMenuLogic : MonoBehaviour
{
    public void GoToMenu()
    {
        // Change scene
        SceneLoader.Instance.ChangeScene(SceneID.MAIN_MENU);
    }
}

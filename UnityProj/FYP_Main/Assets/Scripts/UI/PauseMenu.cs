using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private static bool _isPaused;
    [SerializeField] private GameObject _pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        _isPaused = false;
    }

    #region pause game functionality
    // Set the pausing of the game manually
    public void PauseGame(bool newPauseState)
    {
        if (newPauseState)
        {
            _isPaused = true;
            _pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            _isPaused = false;
            _pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    // Set the pausing of the game automatically
    public void PauseGameSwitch()
    {
        if (_isPaused)
        {
            PauseGame(false);
        }
        else
        {
            PauseGame(true);
        }
    }
    #endregion

    public void SwitchToMainMenu()
    {
        SceneLoader.Instance.ChangeScene(SceneID.MAIN_MENU);
        PauseGame(false);
    }
}

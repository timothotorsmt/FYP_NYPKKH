using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.SceneManagement;

public class SceneTransitionPopup : Interactable
{
    // Scene to go to
    public SceneID _sceneIdToGo;

    public override void Interact()
    {
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.ChangeScene(_sceneIdToGo, true);
        }
    }
}

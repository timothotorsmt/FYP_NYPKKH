using Core.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesLogic : MonoBehaviour
{
    public void GoBackToHub()
    {
        SceneLoader.Instance.ChangeScene(SceneID.HUB_WONDERLAND);
    }
}

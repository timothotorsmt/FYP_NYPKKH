using System.Collections;
using System.Collections.Generic;
using Common.DesignPatterns;
using UnityEngine;

public class ApplicationManager : SingletonPersistent<ApplicationManager>
{
    private void OnApplicationPause()
    {
        PlayerDataSaver.SaveCurrentData();
    }
}

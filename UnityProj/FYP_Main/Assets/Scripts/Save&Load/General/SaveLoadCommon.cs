using System.Collections;
using System.Collections.Generic;
using Common.DesignPatterns;
using UnityEngine;

public class SaveLoadCommon : MonoBehaviour
{
    public void SetLoadedData()
    {
        // Call the function from the GameDataManager to load data
        // This should not call when there is no data
        if (GameDataManager.LoadData<SampleData>(GameDataManager.DataID.SAMPLE_ID) != null)
        {
            sampleData = GameDataManager.LoadData<SampleData>(GameDataManager.DataID.SAMPLE_ID);
        }
    }

    public void SaveCurrentData()
    {
        // Call the function from the GameDataManager to save data
        GameDataManager.SaveData(sampleData);
    }
}

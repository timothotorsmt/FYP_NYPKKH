using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SampleDataSaver : MonoBehaviour
{
    // Create an instance of the data
    public static SampleData sampleData = new SampleData();

    public void Start()
    {
        // Set the file path
        string saveFile = Path.Combine(Application.persistentDataPath, "Data") + "/sampledata.json";
        sampleData.FilePath = saveFile;
    }

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

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SampleData2Saver : MonoBehaviour
{
    public static SampleData2 sampleData2 = new SampleData2();

    public void Start()
    {
        string saveFile = Path.Combine(Application.persistentDataPath, "Data") + "/sampledata2.json";
        sampleData2.FilePath = saveFile;
    }

    public void SetLoadedData()
    {
        if (GameDataManager.LoadData<SampleData2>(GameDataManager.DataID.SAMPLE_ID2) != null)
        {
            sampleData2 = GameDataManager.LoadData<SampleData2>(GameDataManager.DataID.SAMPLE_ID2);
        }
    }

    public void SaveCurrentData()
    {
        GameDataManager.SaveData(sampleData2);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Common.DesignPatterns;


public class GameDataManager : SingletonPersistent<GameDataManager>
{
    // Write the databases here
    private static Dictionary<DataID, GameDataBase> _fileMetaData = new Dictionary<DataID, GameDataBase>
    {
        { DataID.SAMPLE_ID, SampleDataSaver.sampleData },
        { DataID.SAMPLE_ID2, SampleData2Saver.sampleData2 }
    };

    // Keys to the databases in the dictionary
    public enum DataID
    {
        SAMPLE_ID,
        SAMPLE_ID2,
    }

    public static void SaveData(GameDataBase gameData)
    {
        DataSaver.WriteFile(gameData);
    }

    public static type LoadData<type>(DataID dataID)
    {
        string filePath = _fileMetaData[dataID].FilePath;
        string aesKey = _fileMetaData[dataID].GetAesKey();
        PlayerPrefs.SetString("key", aesKey);
        type dataObject = DataSaver.ReadFile<type>(filePath);
        return dataObject;
    }

    //public void DeleteAllData()
    //{

    //}

}

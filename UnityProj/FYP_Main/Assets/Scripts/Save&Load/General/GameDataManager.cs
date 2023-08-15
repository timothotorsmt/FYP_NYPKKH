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
        { DataID.SAMPLE_ID2, SampleData2Saver.sampleData2 },
        { DataID.PLAYER_DATA, PlayerDataSaver.playerData },
        { DataID.PLAYER_SCORES, PlayerScoresSaver.playerScores }
    };

    // Keys to the databases in the dictionary
    public enum DataID
    {
        SAMPLE_ID,
        SAMPLE_ID2,
        PLAYER_DATA,
        PLAYER_SCORES,
    }

    public static void SaveData(GameDataBase gameData)
    {
        DataSaver.WriteFile(gameData);
    }

    public static type LoadData<type>(DataID dataID)
    {
        string filePath = _fileMetaData[dataID].FilePath;
        type dataObject = DataSaver.ReadFile<type>(filePath);
        return dataObject;
    }

    //public void DeleteAllData()
    //{

    //}

}

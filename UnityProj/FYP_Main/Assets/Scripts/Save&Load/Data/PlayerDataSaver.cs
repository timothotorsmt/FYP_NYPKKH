using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDataSaver : MonoBehaviour
{
    // Create an instance of the data
    public static PlayerData playerData = new PlayerData();

    public void Awake()
    {
        // Set the file path
        string saveFile = Path.Combine(Application.persistentDataPath, "Data") + "/playerdata.json";
        playerData.FilePath = saveFile;
    }

    public static bool CheckIfFileExists()
    {
        return File.Exists(playerData.FilePath);
    }

    public static void SetLoadedData()
    {
        // Call the function from the GameDataManager to load data
        // This should not call when there is no data
        if (GameDataManager.LoadData<PlayerData>(GameDataManager.DataID.PLAYER_DATA) != null)
        {
            playerData = GameDataManager.LoadData<PlayerData>(GameDataManager.DataID.PLAYER_DATA);
        }
    }

    public static void SaveCurrentData()
    {
        Debug.Log(playerData.FilePath);

        // Call the function from the GameDataManager to save data
        GameDataManager.SaveData(playerData);
    }
}

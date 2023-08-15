using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerScoresSaver : MonoBehaviour
{
    // Create an instance of the data
    public static PlayerScores playerScores = new PlayerScores();

    public void Start()
    {
        // Set the file path
        string saveFile = Path.Combine(Application.persistentDataPath, "Data") + "/playerscores.json";
        playerScores.FilePath = saveFile;
    }

    public static void SetLoadedData()
    {
        // Call the function from the GameDataManager to load data
        // This should not call when there is no data
        if (GameDataManager.LoadData<PlayerData>(GameDataManager.DataID.PLAYER_SCORES) != null)
        {
            playerScores = GameDataManager.LoadData<PlayerScores>(GameDataManager.DataID.PLAYER_SCORES);
        }
    }

    public static void SaveCurrentData()
    {
        // Call the function from the GameDataManager to save data
        GameDataManager.SaveData(playerScores);
    }
}

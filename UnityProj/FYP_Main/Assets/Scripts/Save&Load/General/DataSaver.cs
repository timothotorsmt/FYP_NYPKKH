using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Security.Cryptography;

[System.Serializable]
public class DataSaver : GameDataManager
{
    // FileStream used for reading and writing files.
    private static FileStream dataStream;

    private new void Awake()
    {
        // Create the data folder if it doesn't exist
        string folderPath = Path.Combine(Application.persistentDataPath, "Data");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }

    public static void WriteFile(GameDataBase gameData)
    {
        if (!File.Exists(gameData.FilePath))
        {
            // Create the data folder if it doesn't exist
            string folderPath = Path.Combine(Application.persistentDataPath, "Data");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        // Serialize the object into JSON and save string.
        string jsonData = JsonUtility.ToJson(gameData);

        // Write all the data to the file
        File.WriteAllText(gameData.FilePath, jsonData);
    }

    public static type ReadFile<type>(string FilePath)
    {
        string fileContents = "";
        // Does the file exist?
        if (File.Exists(FilePath))
        {
            // Read the entire file and save its contents.
            fileContents = File.ReadAllText(FilePath);
        }
        else
        {
            // Create the file
            File.Create(FilePath);
        }
        // Conver the contents from JSON to an object
        type dataInFile = JsonUtility.FromJson<type>(fileContents);

        // Return the object
        return dataInFile;
    }

    //public void DeleteAllData()
    //{

    //}
}

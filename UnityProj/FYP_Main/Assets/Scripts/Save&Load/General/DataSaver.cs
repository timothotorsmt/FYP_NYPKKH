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
        // Create new AES instance.
        Aes iAes = Aes.Create();

        // Update the internal key.
        byte[] _savedKey = iAes.Key;

        // Convert the byte[] into a Base64 String.
        gameData.SetAesKey(System.Convert.ToBase64String(_savedKey));

        // Update the PlayerPrefs
        PlayerPrefs.SetString("key", gameData.GetAesKey());

        // Create a FileStream for creating files.
        dataStream = new FileStream(gameData.FilePath, FileMode.Create);

        // Save the new generated IV.
        byte[] inputIV = iAes.IV;

        // Write the IV to the FileStream unencrypted.
        dataStream.Write(inputIV, 0, inputIV.Length);

        // Create CryptoStream, wrapping FileStream.
        CryptoStream iStream = new CryptoStream(dataStream, iAes.CreateEncryptor(iAes.Key, iAes.IV), CryptoStreamMode.Write);

        // Create StreamWriter, wrapping CryptoStream.
        StreamWriter sWriter = new StreamWriter(iStream);

        // Serialize the object into JSON and save string.
        string jsonData = JsonUtility.ToJson(gameData);

        // Write to the innermost stream (which will encrypt).
        sWriter.Write(jsonData);

        // Close StreamWriter.
        sWriter.Close();

        // Close CryptoStream.
        iStream.Close();

        // Close FileStream.
        dataStream.Close();
    }

    public static type ReadFile<type>(string FilePath)
    {
        string fileContents = "";
        // Does the file exist?
        if (File.Exists(FilePath) && PlayerPrefs.HasKey("key"))
        {
            // Update key based on PlayerPrefs
            // (Convert the String into a Base64 byte[] array.)
            byte[] _savedKey = System.Convert.FromBase64String(PlayerPrefs.GetString("key"));

            // Create FileStream for opening files.
            dataStream = new FileStream(FilePath, FileMode.Open);

            // Create new AES instance.
            Aes oAes = Aes.Create();

            // Create an array of correct size based on AES IV.
            byte[] outputIV = new byte[oAes.IV.Length];

            // Read the IV from the file.
            dataStream.Read(outputIV, 0, outputIV.Length);

            // Create CryptoStream, wrapping FileStream
            CryptoStream oStream = new CryptoStream(dataStream, oAes.CreateDecryptor(_savedKey, outputIV), CryptoStreamMode.Read);

            // Create a StreamReader, wrapping CryptoStream
            StreamReader reader = new StreamReader(oStream);

            // Read the entire file into a String value.
            fileContents = reader.ReadToEnd();
            // Always close a stream after usage.
            reader.Close();
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

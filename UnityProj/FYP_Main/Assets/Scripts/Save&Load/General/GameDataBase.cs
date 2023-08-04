using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataBase
{
    public string FilePath;
    private string _aesKey;

    public string GetAesKey()
    {
        return _aesKey;
    }

    public void SetAesKey(string newKey)
    {
        _aesKey = newKey;
    }

}

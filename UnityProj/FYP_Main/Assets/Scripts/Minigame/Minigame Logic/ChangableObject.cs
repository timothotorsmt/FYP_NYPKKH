using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class changableObject<T> where T : struct, System.Enum
{
    public T TaskOnChange;
    public GameObject GameObjectToChange;
}

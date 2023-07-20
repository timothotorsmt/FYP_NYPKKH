using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class contains a changeable object, which can be changed based on an enumerator
// Works like a data container, should be placed in another class
[System.Serializable]
public class changableObject<T> where T : struct, System.Enum
{
    public T TaskOnChange;
    public GameObject GameObjectToChange;
}

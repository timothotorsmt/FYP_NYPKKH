using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class changableSprite<T> where T : struct, System.Enum
{
    public T TaskOnChange;
    public Sprite SpriteToChange;
}

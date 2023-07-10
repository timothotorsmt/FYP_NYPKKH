using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Works the same as a changable object, but changes the sprite instead of the whole object
// Allows you to change stuff without having to create a whole new object
[System.Serializable]
public class changableSprite<T> where T : struct, System.Enum
{
    public T TaskOnChange;
    public Sprite SpriteToChange;
}

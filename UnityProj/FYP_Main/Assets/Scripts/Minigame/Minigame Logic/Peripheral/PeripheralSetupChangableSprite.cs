using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using UnityEngine.UI;

public class PeripheralSetupChangableSprite : MonoBehaviour
{
    // List out all needed sprites
    [SerializeField] private List<changableSprite<PeripheralSetupTasks>> _changedSprites;
    
    // Sprite to be changed
    private Image _changableImage;

    // Start()
    public void Start() 
    {
        _changableImage = GetComponent<Image>();
        PeripheralSetupTaskController.Instance.CurrentTask.Value.Subscribe(State => {
            if (_changedSprites.Where(s => s.TaskOnChange == State).Select(s => s.SpriteToChange).Count() > 0) 
            {
                _changableImage.sprite = _changedSprites.Where(s => s.TaskOnChange == State).Select(s => s.SpriteToChange).First();
            }
        });
    }
}


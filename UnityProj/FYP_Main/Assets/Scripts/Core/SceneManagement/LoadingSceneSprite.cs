using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using Common.DesignPatterns;

public class LoadingSceneSprite : MonoBehaviour
{
    // Sprite to rotate
    // TODO: make the code more modular
    [SerializeField] private Image _loadingSprite; 

    void Start()
    {
        _loadingSprite.transform.DORotate(new Vector3(0.0f, 0.0f, -360.0f), 1.0f)
                                .SetLoops(-1, LoopType.Restart)
                                .SetRelative(true)
                                .SetEase(Ease.Linear);
    }
}

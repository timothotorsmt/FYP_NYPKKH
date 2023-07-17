using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using Common.DesignPatterns;

public class LoadingSceneSprite : MonoBehaviour
{
    [SerializeField] private Image _loadingSprite; 
    // Start is called before the first frame update
    void Start()
    {
        _loadingSprite.transform.DORotate(new Vector3(0.0f, 0.0f, -180.0f), 1.0f).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }
}

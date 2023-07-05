using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BlinkingPicture : MonoBehaviour
{
    [SerializeField] private bool _isBlinking = true;
    [SerializeField, Range(0, 10)] private float _blinkIntervals = 1.0f;
    [SerializeField,　Range(0, 10)] private float _delay = 0.0f;
    private Image _blinkingImage;

    private Sequence seq;

    private void Start()
    {
        _blinkingImage = GetComponent<Image>();
        if (_isBlinking)
        {
            StartBlinking();
        }
    }
    
    private void OnDisable()
    {
        DOTween.Kill(this);
    }
    
    public void SetBlinking(bool isNewBlinking)
    {
        _isBlinking = isNewBlinking;
        if (_isBlinking)
        {
            StartBlinking();
        }
        else 
        {
            if (seq.IsActive())
            {
                seq.Kill();
            }
        }
    }

    private void StartBlinking()
    {
        seq = DOTween.Sequence();
        seq.Append(_blinkingImage.DOColor(Color.white, _blinkIntervals / 2));
        seq.Append(_blinkingImage.DOColor(Color.gray, _blinkIntervals / 2));
        seq.AppendInterval(_delay);
        seq.SetLoops(-1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

// As requested by the clients, they wanted some pictures to be able to "blink" to act as guides for players towards important things
// Not really as blinking more so as in tweening between 2 colours
public class BlinkingPicture : MonoBehaviour
{
    [Header("Blinking Settings")]
    [SerializeField] private bool _isBlinking = true;
    [SerializeField, Range(0, 10)] private float _blinkIntervals = 1.0f;
    [SerializeField, Range(0, 10)] private float _delay = 0.0f;

    [Header("Decor Settings")]
    [SerializeField] private Color _changeColor = Color.white;
    [SerializeField] private Color _originalColor = Color.white;
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
        // Remove on disable
        // Prevents an error from showing up
        DOTween.Kill(this);
    }
    
    // Start the blinking effect
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
        seq.Append(_blinkingImage.DOColor(_originalColor, _blinkIntervals / 2));
        seq.Append(_blinkingImage.DOColor(_changeColor, _blinkIntervals / 2));
        seq.AppendInterval(_delay);
        seq.SetLoops(-1);
    }
}

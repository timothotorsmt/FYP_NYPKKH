using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class FadeInOut : MonoBehaviour
{
    private CanvasGroup _currentPanelCanvasGroup;
    [SerializeField, Range(0, 5)] private float _fadeDuration = 1.0f;

    void Start()
    {
        _currentPanelCanvasGroup = GetComponent<CanvasGroup>();
    }

    public float GetFloatDuration()
    {
        return _fadeDuration;
    }

    public void FadeIn()
    {
        gameObject.SetActive(true);

        if (_currentPanelCanvasGroup == null)
        {
            _currentPanelCanvasGroup = GetComponent<CanvasGroup>();
        }

        // Set image to transparent
        _currentPanelCanvasGroup.alpha = 0;
        _currentPanelCanvasGroup.DOFade(1.0f, _fadeDuration);
    }

    public void FadeOut()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_currentPanelCanvasGroup.DOFade(0.0f, _fadeDuration));
        seq.AppendCallback(() => gameObject.SetActive(false));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    Sequence _sequence;  
    [SerializeField] private UnityEvent _afterDisplayEvent;
    [SerializeField] private Image _titleImage;
    FadeInOut _fade;

    private void Awake()
    {
        
    }

    public void StartTitleSequence()
    {
        _sequence = DOTween.Sequence();
        _fade = GetComponent<FadeInOut>();
        _fade.FadeIn();
        _titleImage.color = new Color(1,1,1,0);
        StartCoroutine(DisplayTitle());
    }

    public IEnumerator DisplayTitle()
    {
        yield return new WaitForSeconds(_fade.GetFloatDuration());
        
        // do something
        _sequence.Append(_titleImage.DOFade(1.0f, 1.0f));

        yield return _sequence.WaitForCompletion();
        yield return new WaitForSeconds(3.0f); // Temporary.

        _afterDisplayEvent.Invoke();
    }
}

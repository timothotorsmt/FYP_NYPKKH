using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class TitleScreen : MonoBehaviour
{
    Sequence _sequence;  
    [SerializeField] private UnityEvent _afterDisplayEvent;
    FadeInOut _fade;

    private void Awake()
    {
        
    }

    public void StartTitleSequence()
    {
        _sequence = DOTween.Sequence();
        _fade = GetComponent<FadeInOut>();
        _fade.FadeIn();
        StartCoroutine(DisplayTitle());
    }

    public IEnumerator DisplayTitle()
    {
        yield return new WaitForSeconds(_fade.GetFloatDuration());
        
        // do something

        yield return _sequence.WaitForCompletion();
        yield return new WaitForSeconds(3.0f); // Temporary.

        _afterDisplayEvent.Invoke();
    }
}

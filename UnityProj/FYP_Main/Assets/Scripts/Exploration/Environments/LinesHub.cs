using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LinesHub : MonoBehaviour
{
    // heartbeat pulse
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _heart;
    [SerializeField] private GameObject _wheelchair;
    [SerializeField] private GameObject _bed;
    private Sequence seq;
    Vector3 _originalScale;

    void Start()
    {
        _originalScale = gameObject.transform.localScale;
        seq = DOTween.Sequence();

        seq.Append(_heart.transform.DOScale(1.1f * _originalScale.x, 0.2f).SetEase(Ease.InExpo));
        seq.Join(_background.transform.DOScale(1.005f * _originalScale.x, 0.2f).SetEase(Ease.InExpo).SetDelay(0.05f));
        seq.Join(_wheelchair.transform.DOMoveX(_wheelchair.transform.position.x - 0.1f, 0.2f).SetEase(Ease.InExpo).SetDelay(0.03f));
        seq.Join(_bed.transform.DOMoveX(_bed.transform.position.x + 0.05f, 0.2f).SetEase(Ease.InExpo).SetDelay(0.03f));
        seq.Append(_heart.transform.DOScale(_originalScale.x, 1.0f).SetEase(Ease.OutExpo));
        seq.Join(_background.transform.DOScale(_originalScale.x, 1.0f).SetEase(Ease.OutExpo).SetDelay(0.05f));
        seq.Join(_wheelchair.transform.DOMoveX(_wheelchair.transform.position.x + 0.1f, 1.0f).SetEase(Ease.OutExpo).SetDelay(0.03f));
        seq.Join(_bed.transform.DOMoveX(_bed.transform.position.x - 0.05f, 1.0f).SetEase(Ease.OutExpo).SetDelay(0.03f));

        seq.SetLoops(-1);
    }
}

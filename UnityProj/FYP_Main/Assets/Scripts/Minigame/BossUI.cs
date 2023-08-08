using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.DesignPatterns;
using UnityEngine.Events;

public class BossUI : Singleton<BossUI>
{
    [SerializeField] private GameObject _overallContainer;
    [SerializeField] private GameObject _shiftStartText;
    [SerializeField] private GameObject _shiftOverText;


    public void SetStart()
    {
        _shiftStartText.SetActive(true);
        _shiftOverText.SetActive(false);
        _overallContainer.SetActive(true);
        StartCoroutine(fadeOut());
    }

    public IEnumerator fadeOut()
    {
        yield return new WaitForSeconds(2.0f);
        _overallContainer.GetComponent<FadeInOut>().FadeOut();
    }

    public void SetEnd()
    {
        _shiftStartText.SetActive(false);
        _shiftOverText.SetActive(true);
        _overallContainer.GetComponent<FadeInOut>().FadeIn();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using TMPro;

public class MinigamePerformanceUI : MonoBehaviour
{
    [System.Serializable]
    private class ResultSprite
    {
        public Grade SpriteGrade;
        public Sprite ResultItem; // Come up with better names please
    }

    // Controls the UI aspect of the performance scoreboard page
    [Header("UI gameobjects")]
    [SerializeField] private GameObject _performanceReviewScreen; // The main panel for the minigame result screen
    [SerializeField] private GameObject _minigameTitle;
    [SerializeField] private GameObject _scoreHeader;
    [SerializeField] private GameObject _resultObject;
    [SerializeField] private GameObject _levelUpObject;
    [SerializeField] private GameObject _buttons;
    [SerializeField] private TextMeshProUGUI _titleText;

    [Header("Result Sprites")]
    [SerializeField] private List<ResultSprite> _resultSprites;
    [SerializeField] private Image _resultDisplay; // The one that shows the result

    [Header("Level up")]
    [SerializeField] private Slider _levelUpSlider;

    private Sequence _seq; // The animation sequence

    private void Start()
    {
    }

    public void DisplayResult()
    {
        _performanceReviewScreen.SetActive(false);
        _minigameTitle.SetActive(false);
        _scoreHeader.SetActive(false);
        _buttons.SetActive(false);
        _resultObject.SetActive(false);
        _levelUpObject.SetActive(false);

        _seq = DOTween.Sequence();

        _seq.AppendCallback(() => _performanceReviewScreen.SetActive(true));
        _seq.AppendInterval(0.5f);
        _seq.AppendCallback(() => _minigameTitle.SetActive(true));
        _seq.AppendInterval(1.0f);
        _seq.AppendCallback(() => _scoreHeader.SetActive(true));
        _seq.AppendInterval(0.5f);
        _seq.AppendCallback(() => _resultObject.SetActive(true));
        _seq.AppendInterval(0.5f);
        _seq.AppendCallback(() => _levelUpObject.SetActive(true));
        _seq.AppendInterval(1.0f);
        _seq.AppendCallback(() => _buttons.SetActive(true));
    }

    public void GetMinigameInfo(MinigameInfo _currentMinigameInfo)
    {
        _titleText.text = _currentMinigameInfo.minigameName;
    }

    public void AssignResult(Grade newGrade)
    {
        // Get the image based on the grade given
        _resultDisplay.sprite = _resultSprites.Where(x => x.SpriteGrade == newGrade).Select(x => x.ResultItem).FirstOrDefault();
    }
}

using PatientManagement;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Common.DesignPatterns;
using System.Linq;

// The main logic class behind the line boss section
public class LinesBossLogic : SingletonPersistent<LinesBossLogic>
{
    public List<Button> _minigameList;
    [SerializeField] private UnityEvent _bossOverEvent;
    [SerializeField, Range(0, 5)] private float _minWaitTime = 3.0f;
    [SerializeField, Range(1, 10)] private float _maxWaitTime = 8.0f;

    private static System.Random rng = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        _minigameList = new List<Button>();
        Boss360Buttons.Instance.DisableAllButtons();
        RandomiseMinigames();
        _minigameList[0].gameObject.SetActive(true);
    }

    private void RandomiseMinigames()
    {
        // CVL or showers?
        int RandNum = Random.Range(0, 2);
        // Temporarily remove the shower minigame (because)
        if (false)
        {
            //_minigameList.Add(Boss360Buttons.Instance._bed5Button);
            //Boss360Buttons.Instance._bed5Button.onClick.AddListener(() => { MinigameManager.Instance.StartMinigame(MinigameID.CVL_SHOWER); Boss360Buttons.Instance.HideButtons(); });
        }
        else
        {
            _minigameList.Add(Boss360Buttons.Instance._doorButton);
            Boss360Buttons.Instance._doorButton.onClick.AddListener(() => { MinigameManager.Instance.StartMinigame(MinigameID.CVL_PREREQUISITE); Boss360Buttons.Instance.HideButtons();});
        }

        _minigameList.Add(Boss360Buttons.Instance._bed1Button);
        Boss360Buttons.Instance._bed1Button.onClick.AddListener(() => { MinigameManager.Instance.StartMinigame(MinigameID.OCCLUSION_1, Difficulty.LEVEL_10); Boss360Buttons.Instance.HideButtons();});
        _minigameList.Add(Boss360Buttons.Instance._bed4Button);
        Boss360Buttons.Instance._bed4Button.onClick.AddListener(() => { MinigameManager.Instance.StartMinigame(MinigameID.OCCLUSION_1, Difficulty.LEVEL_10); Boss360Buttons.Instance.HideButtons();});

        _minigameList.Add(Boss360Buttons.Instance._bed2Button);
        Boss360Buttons.Instance._bed2Button.onClick.AddListener(() => { MinigameManager.Instance.StartMinigame(MinigameID.OCCLUSION_1); Boss360Buttons.Instance.HideButtons(); });

        _minigameList.Add(Boss360Buttons.Instance._bed3Button);
        Boss360Buttons.Instance._bed3Button.onClick.AddListener(() => { MinigameManager.Instance.StartMinigame(MinigameID.PERIPHERAL_SETUP); Boss360Buttons.Instance.HideButtons(); });

        // Have all the minigames now, randomise them
        _minigameList = _minigameList.OrderBy(a => rng.Next()).ToList();
    }

    public void FinishMinigame()
    {
        _minigameList[0].gameObject.SetActive(false);
        _minigameList.Remove(_minigameList[0]);

        if (_minigameList.Count != 0)
        {
            // Start Coroutine 
            StartCoroutine(SetNextTaskTimer());
        }
        else
        {
            BossOver();
        }
    }

    public IEnumerator SetNextTaskTimer()
    {
        float waitTime = Random.Range(_minWaitTime, _maxWaitTime);
        yield return new WaitForSeconds(waitTime);
        _minigameList[0].gameObject.SetActive(true);

    }

    public void BossOver()
    {
        MinigameSceneController.Instance.GoBackToHub();
        Boss360Buttons.Instance.DisableAllButtons();
        Destroy(Boss360Buttons.Instance);
        Destroy(this);
    }
}

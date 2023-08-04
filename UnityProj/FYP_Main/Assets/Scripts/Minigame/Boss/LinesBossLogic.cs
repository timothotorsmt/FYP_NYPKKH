using PatientManagement;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using Common.DesignPatterns;
using System.Linq;

public class LinesBossLogic : SingletonPersistent<LinesBossLogic>
{
    public List<Button> _minigameList;

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
        if (RandNum == 1)
        {
            _minigameList.Add(Boss360Buttons.Instance._bed5Button);
            Boss360Buttons.Instance._bed5Button.onClick.AddListener(() => { MinigameManager.Instance.StartMinigame(MinigameID.CVL_SHOWER); });
        }
        else
        {
            _minigameList.Add(Boss360Buttons.Instance._doorButton);
            Boss360Buttons.Instance._doorButton.onClick.AddListener(() => { MinigameManager.Instance.StartMinigame(MinigameID.CVL_PREREQUISITE); });
        }

        _minigameList.Add(Boss360Buttons.Instance._bed1Button);
        Boss360Buttons.Instance._bed1Button.onClick.AddListener(() => { MinigameManager.Instance.StartMinigame(MinigameID.OCCLUSION_1, Difficulty.LEVEL_10); });
        _minigameList.Add(Boss360Buttons.Instance._bed4Button);
        Boss360Buttons.Instance._bed4Button.onClick.AddListener(() => { MinigameManager.Instance.StartMinigame(MinigameID.OCCLUSION_1, Difficulty.LEVEL_10); });

        _minigameList.Add(Boss360Buttons.Instance._bed2Button);
        Boss360Buttons.Instance._bed2Button.onClick.AddListener(() => { MinigameManager.Instance.StartMinigame(MinigameID.OCCLUSION_1); });

        _minigameList.Add(Boss360Buttons.Instance._bed3Button);
        Boss360Buttons.Instance._bed3Button.onClick.AddListener(() => { MinigameManager.Instance.StartMinigame(MinigameID.PERIPHERAL_SETUP); });

        _minigameList = _minigameList.OrderBy(a => rng.Next()).ToList();
    }

    public void FinishMinigame()
    {
        _minigameList[0].gameObject.SetActive(false);
        _minigameList.Remove(_minigameList[0]);

        if (_minigameList.Count != 0)
        {
            _minigameList[0].gameObject.SetActive(true);
        }
        else
        {
            MinigameSceneController.Instance.GoBackToHub();
            Boss360Buttons.Instance.DisableAllButtons();
            Destroy(this);
        }
    }
}

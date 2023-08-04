using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common.DesignPatterns;

public class Boss360Buttons : Singleton<Boss360Buttons>
{
    public Button _bed1Button;
    public Button _bed2Button;
    public Button _bed3Button;
    public Button _bed4Button;
    public Button _bed5Button;
    public Button _doorButton;

    public void DisableAllButtons()
    {
        _bed1Button.gameObject.SetActive(false);
        _bed1Button.onClick.RemoveAllListeners();
        _bed2Button.gameObject.SetActive(false);
        _bed2Button.onClick.RemoveAllListeners();
        _bed3Button.gameObject.SetActive(false);
        _bed3Button.onClick.RemoveAllListeners();
        _bed4Button.gameObject.SetActive(false);
        _bed4Button.onClick.RemoveAllListeners();
        _bed5Button.gameObject.SetActive(false);
        _bed5Button.onClick.RemoveAllListeners();
        _doorButton.gameObject.SetActive(false);
        _doorButton.onClick.RemoveAllListeners();
    }
}

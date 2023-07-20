using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

// The class that controls the BBraun machine's audio system
public class BBraunAudio : MonoBehaviour
{
    private bool _isCurrentlyRunning;

    // Start is called before the first frame update
    public void StartAlarm()
    {
        _isCurrentlyRunning = true;
        StartCoroutine(BBraunAlarm());
    }

    // Play BBraun Alarm
    private IEnumerator BBraunAlarm()
    {
        AudioController.Instance.PlayAudio(SoundUID.BBRAUN_OPERATING_ALARM);
        yield return new WaitForSeconds(5.0f);
        if (_isCurrentlyRunning == true)
        {
            StartCoroutine(BBraunAlarm());
        }
    }

    // Mute alarm
    public void MuteAlarm()
    {
        _isCurrentlyRunning = false;
        AudioController.Instance.StopAudio(SoundUID.BBRAUN_OPERATING_ALARM);
    }
}

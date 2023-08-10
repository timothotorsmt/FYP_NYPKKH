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

    private void OnDisable()
    {
        AudioController.Instance.StopAudio(SoundUID.BBRAUN_OPERATING_ALARM);
    }

    // Play BBraun Alarm
    private IEnumerator BBraunAlarm()
    {
        AudioController.Instance.PlayAudio(SoundUID.BBRAUN_OPERATING_ALARM);
        // Wait for 5 seconds before playing the next alarm
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

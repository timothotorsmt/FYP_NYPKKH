using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class AudioInteractable : Popup
{
    [SerializeField] protected SoundUID _soundID;

    private bool isPlaying = false;

    public override void Interact()
    {
        if (!isPlaying) {
            AudioController.Instance.PlayAudio(_soundID);
            isPlaying = true;
        } else 
        {
            AudioController.Instance.StopAudio(_soundID);
            isPlaying = false;
        }
    }
}

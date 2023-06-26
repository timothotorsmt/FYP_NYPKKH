using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    [System.Serializable]
    public class AudioObject
    {
        public SoundUID AudioID;
        public AudioClip Clip;
    }

    public enum AudioAction
    {
        START,
        STOP,
        RESTART
    }

    // Add audio type
    [System.Serializable]
    public class AudioTrack 
    {
        public AudioSource Source; // Source of audio to play
        public AudioObject[] Audio; // Audio to play
    }

    [System.Serializable]
    public class AudioJob
    {
        public AudioAction Action;
        public SoundUID AudioID;
        public bool Loop;
        public bool Fade;
        public float FadeDuration;
        public WaitForSeconds Delay;

        public AudioJob (AudioAction action, SoundUID audioID, bool loop = false, bool fade = false, float delay = 0f, float fadeDuration = 0f)
        {
            Action = action;
            AudioID = audioID;
            Loop = loop;
            Fade = fade;
            FadeDuration = fadeDuration;
            Delay = (delay > 0f) ? new WaitForSeconds(delay) : null;
        }
    }
}


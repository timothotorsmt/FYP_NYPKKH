using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(menuName = "Custom data containers/Audio list")]
    public class AudioList : ScriptableObject
    {
        public List<AudioTrack> _tracks;
    }
}

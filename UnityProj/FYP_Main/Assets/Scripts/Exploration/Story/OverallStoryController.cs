using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Extention;

public class OverallStoryController : StoryManager<OverallStoryBeats>
{
}

public enum OverallStoryBeats
{
    INTRODUCTION,
    TUTORIAL,
    NUM_STORY_BEATS
}

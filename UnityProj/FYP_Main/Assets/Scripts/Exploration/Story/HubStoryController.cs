using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Extention;
using UniRx;
using UnityEngine.Events;

public class HubStoryController : StoryManager<NormalHubStoryBeats>
{
    [SerializeField] private UnityEvent _afterIntroductionEvent;
    [SerializeField] private UnityEvent _afterMonologueEvent;
    [SerializeField] private UnityEvent _finishMonologueEvent;

    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Instance._overallStoryController.CurrentStoryBeat.Value.Subscribe(state => {
            switch (state)
            {
                case OverallStoryBeats.INTRODUCTION:
                    // Start off with the introductory dialogue
                    // idk why this is needed
                    MarkCurrentStoryBeatAsDone();
                    ChatGetter.Instance.StartChat("#HUBINTA", _afterIntroductionEvent);
                    break;
            }
        });
    }

    public void IntroMonologue()
    {
        MarkCurrentStoryBeatAsDone();
        ChatGetter.Instance.StartChat("#HUBINTC", _afterMonologueEvent);
    }

    public void IntroFinishMonologue()
    {
        MarkCurrentStoryBeatAsDone();
        ChatGetter.Instance.StartChat("#HUBINTD", _finishMonologueEvent);
    }

    public void IntroFinish()
    {
        MarkCurrentStoryBeatAsDone();
        PlayerManager.Instance._overallStoryController.MarkCurrentStoryBeatAsDone();
    }
}

public enum NormalHubStoryBeats
{
    INTRODUCTION,
    INTRODUCTION_WITH_ALICE,
    INTRODUCTION_WITH_ROOM,
    TUTORIAL_MOVEMENT,
    TUTORIAL_INTERACTION,
    TUTORIAL_SPEECH,
    GOSSIP,
    // "Sleep" Sequence
    SLEEP,
}
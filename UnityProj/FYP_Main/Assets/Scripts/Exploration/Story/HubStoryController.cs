using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Extention;
using UniRx;
using DG.Tweening;
using UnityEngine.Events;
using Core.SceneManagement;

public class HubStoryController : StoryManager<NormalHubStoryBeats>
{
    [SerializeField] private UnityEvent _afterIntroductionEvent;
    [SerializeField] private UnityEvent _afterMonologueEvent;
    [SerializeField] private UnityEvent _finishMonologueEvent;

    [SerializeField] private UnityEvent _afterGossipEvent;
    [SerializeField] private UnityEvent _skipStoryEvent;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerProgress.Instance != null)
        {
            if (PlayerProgress.Instance.hasDoneStory)
            {
                CurrentStoryBeat.SetValue(NormalHubStoryBeats.MAIN_GAME);
                _skipStoryEvent.Invoke();
            }
        }

        PlayerManager.Instance._overallStoryController.CurrentStoryBeat.Value.Subscribe(state => {
            switch (state)
            {
                case OverallStoryBeats.INTRODUCTION:
                    if (PlayerProgress.Instance != null)
                    {
                        if (!PlayerProgress.Instance.hasDoneStory)
                        {
                            // Start off with the introductory dialogue
                            // idk why this is needed
                            MarkCurrentStoryBeatAsDone();
                            ChatGetter.Instance.StartChat("#HUBINTA", _afterIntroductionEvent);
                            PlayerProgress.Instance.hasDoneStory = true;
                        }
                        PlayerDataSaver.playerData.hasDoneStory = PlayerProgress.Instance.hasDoneStory;
                    }

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
        PlayerManager.Instance._overallStoryController.MarkCurrentStoryBeatAsDone();
        Debug.Log(PlayerManager.Instance._overallStoryController.CurrentStoryBeat.GetValue().ToString());
        MarkCurrentStoryBeatAsDone();
    }

    public void ListenToGossip()
    {
        ChatGetter.Instance.StartChat("#HUBINTB", _afterGossipEvent);
    }

    public void EndTalkScene()
    {
        PlayerManager.Instance._overallStoryController.MarkCurrentStoryBeatAsDone();
    }
}

public enum NormalHubStoryBeats
{
    INTRODUCTION,
    INTRODUCTION_WITH_ALICE,
    TUTORIAL_MOVEMENT,
    TUTORIAL_INTERACTION,
    PETER_TALK,

    MAIN_GAME,
    
    // "Sleep" Sequence
    SLEEP,
}
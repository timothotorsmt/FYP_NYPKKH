using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Extention;
using UniRx;

public class HubStoryController : MonoBehaviour
{
    // The current story beat which the game is following at the current moment
    [HideInInspector] public ReactiveProp<HubStoryBeats> _currentStoryBeat;

    // Start is called before the first frame update
    void Start()
    {
        _currentStoryBeat.Value.Subscribe(state => {
            switch (state)
            {
                case HubStoryBeats.INTRODUCTION:
                    // Start off with the introductory dialogue
                    break;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum HubStoryBeats
{
    INTRODUCTION,
    TUTORIAL_MOVEMENT,
    TUTORIAL_SPEECH,
    GOSSIP,
    // "Sleep" Sequence
    SLEEP,
    // For the rest of the game, this is the current state the hub will be in until the game is completed
    GAME_NOT_COMPLETE,
    // Post game (?)
    GAME_COMPLETE,
}
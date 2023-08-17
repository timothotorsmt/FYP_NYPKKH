using BBraunInfusomat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

// Script to display a single instruction because its specialcase type
public class CheckPeriBBraunInstructions : MonoBehaviour
{
    private bool hasGivenInstructions = false;
    private bool hasGivenParamInstructions = false;
    private bool hasGivenStartInstructions = false;

    [SerializeField] private BBraunIPLogic _bbraunLogic;

    // Not running?????????????????????? On the first time it's being called???????????? WHAT
    public void Start()
    {
        if (MinigameManager.Instance == null)
        {
            return;
        }

        if (MinigameManager.Instance.GetMinigameDifficulty().GameDifficulty != Difficulty.BOSS && MinigameManager.Instance.GetMinigameDifficulty().GameDifficulty != Difficulty.LEVEL_10)
        {
            // Give instructions based on the current UI display
            _bbraunLogic.BBraunState.Value.Subscribe(state =>
            {
                switch (state)
                {
                    case BBraunIPState.START:
                        StartCoroutine(GiveBBraunDialogueLine("#PERIBB"));
                        break;
                }
            });
        }
    }

    public void GiveBBraunInstructions()
    {

        // Ensure current difficulty is not these 2 because they are woah.
        if (MinigameManager.Instance.GetMinigameDifficulty().GameDifficulty != Difficulty.BOSS && MinigameManager.Instance.GetMinigameDifficulty().GameDifficulty != Difficulty.LEVEL_10)
        {
            if (PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.OPEN_DOOR && !hasGivenInstructions)
            {

                hasGivenInstructions = true;
                ChatGetter.Instance.StartChat("#PERIBA");

            }

            if (PeripheralSetupTaskController.Instance.GetCurrentTask() == PeripheralSetupTasks.START_PUMP && !hasGivenStartInstructions)
            {
                hasGivenStartInstructions = true;
                ChatGetter.Instance.StartChat("#PERIBC");

            }
        }
    }

    private IEnumerator GiveBBraunDialogueLine(string ID)
    {
        yield return new WaitForSeconds(0.5f);
        ChatGetter.Instance.StartChat(ID);
    }


}

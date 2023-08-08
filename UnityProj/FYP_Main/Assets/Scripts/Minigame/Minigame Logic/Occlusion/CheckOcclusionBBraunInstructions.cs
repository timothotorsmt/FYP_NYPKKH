using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to display a single instruction because its specialcase type
public class CheckOcclusionBBraunInstructions : MonoBehaviour
{
    private bool hasGivenInstructions = false;
    private bool hasGivenStandbyInstructions = false;

    // Not running?????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????? On the first time it's being called???????????? WHAT
    public void GiveBBraunInstructions()
    {
        if (MinigameManager.Instance == null)
        {
            return;
        }

        // Ensure current difficulty is not these 2 because they are woah.
        if (MinigameManager.Instance.GetMinigameDifficulty().GameDifficulty != Difficulty.BOSS && MinigameManager.Instance.GetMinigameDifficulty().GameDifficulty != Difficulty.LEVEL_10)
        {
            if (OcclusionTaskController.Instance.GetCurrentTask() == OcclusionTasks.MUTE_ALARM && !hasGivenInstructions)
            {
                hasGivenInstructions = true;
                StartCoroutine(GiveBBraunDialogueLine());
            }

            if (OcclusionTaskController.Instance.GetCurrentTask() == OcclusionTasks.PUT_PUMP_ON_STANDBY && !hasGivenStandbyInstructions)
            {
                hasGivenStandbyInstructions = true;
                StartCoroutine(GiveStandbyDialogueLine());
            }
        }
    }

    private IEnumerator GiveBBraunDialogueLine()
    {
        yield return new WaitForSeconds(0.5f);
        ChatGetter.Instance.StartChat("#OCCLRD");
        hasGivenInstructions = false;
    }

    private IEnumerator GiveStandbyDialogueLine()
    {
        yield return new WaitForSeconds(0.5f);
        ChatGetter.Instance.StartChat("#OCCLPD");
        hasGivenStandbyInstructions = false;
    }
}

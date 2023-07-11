using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinigameBase
{
    public class HintSystemBase : MonoBehaviour
    {
        // Base class for the hint system
        [SerializeField] protected MinigameChatGetter _chatGetter; // Chat getter to display the hint dialogue
        [SerializeField] protected GameObject _button; // The physical hint button
        [SerializeField] protected DifficultySettings _minigameDifficulty; // The current minigame difficulty
        [SerializeField, Range(0, 120)] protected float _hintTimer = 10f; // The amount of time the player has to wait for a hint

        protected bool _isRunningHint; // Is currently counting for a hint 

        // The coroutine for counting for the hint timer
        protected IEnumerator HintCounter()
        {
            yield return new WaitForSeconds(_hintTimer);
            _button.SetActive(true);
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinigameBase
{
    public class HintSystemBase : MonoBehaviour
    {
        [SerializeField] protected MinigameChatGetter _chatGetter;
        [SerializeField] protected GameObject _button;
        [SerializeField] protected DifficultySettings _minigameDifficulty;

        protected bool _isRunningHint;

        protected IEnumerator HintCounter()
        {
            yield return new WaitForSeconds(10);
            _button.SetActive(true);
        }

    }
}
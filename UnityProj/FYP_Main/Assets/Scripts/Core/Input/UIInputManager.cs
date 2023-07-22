using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Common.DesignPatterns;

namespace Core.Input
{
    // This class detects the input events, and also notifies the subscribers when input events
    public class UIInputManager : Observer<IInputActions>
    {
        // variables
        private uint? _currentFingerId;
        private bool _isCurrentTap;

        #region check input

        // Update is called once per frame
        void Update()
        {
            // Check for current platform
            if (Application.isEditor)
            {
                // Desktop (using mouse controls)
                // Make sure player is not tapping the UI
                if (UnityEngine.Input.GetMouseButtonDown(0))
                {
                    CallOnStartTap();
                }
                else if (UnityEngine.Input.GetMouseButton(0) && _isCurrentTap)
                {
                    CallOnTap();
                }
                else if (UnityEngine.Input.GetMouseButtonUp(0) && _isCurrentTap)
                {
                    CallOnEndTap();
                }
            }
            else
            {
                if (UnityEngine.Input.touchCount > 0)
                {
                    Touch touch = UnityEngine.Input.GetTouch(0);
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            _currentFingerId = (uint)touch.fingerId;
                            CallOnStartTap();
                            break;
                        case TouchPhase.Moved:
                            // If the current touch finger id does not match the current finger id, chance that the finger has been swapped
                            if (_currentFingerId != (uint)touch.fingerId)
                            {
                                CallOnEndTap();
                                return;
                            }
                            CallOnTap();
                            break;
                        case TouchPhase.Canceled:
                        case TouchPhase.Ended:
                            CallOnEndTap();
                            break;
                    }
                }
                else
                {
                    _isCurrentTap = false;
                }
            }
        }

        #endregion

        #region mass call notifications

        private void CallOnStartTap()
        {
            foreach (IInputActions subscriber in _subscribers)
            {
                subscriber.OnStartTap();
            }
            _isCurrentTap = true;
        }

        private void CallOnTap()
        {
            foreach (IInputActions subscriber in _subscribers)
            {
                subscriber.OnTap();
            }
        }

        private void CallOnEndTap()
        {
            foreach (IInputActions subscriber in _subscribers)
            {
                if (subscriber != null)
                {
                    subscriber.OnEndTap();
                }
            }
            _isCurrentTap = false;
        }

        #endregion
    }

}

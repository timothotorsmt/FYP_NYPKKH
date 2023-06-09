using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Input 
{
    // This class subscribes to all mobile input tap actions
    public interface IInputActions
    {
        // The first frame when the player taps the screen first 
        public void OnStartTap();

        // When tapping
        public void OnTap();

        // When they finish tapping
        public void OnEndTap();
    }
}

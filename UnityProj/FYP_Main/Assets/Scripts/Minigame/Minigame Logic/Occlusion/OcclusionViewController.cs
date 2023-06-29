using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class controls the UI system of the minigame
public class OcclusionViewController : UIViewController<OcclusionView>
{

    // Override the run function
    protected override void Start()
    {
        base.Start();

        // Handle patient stuff here

        // Set up any base things at the same time
    }

    #region UI button callbacks
    
    public void ChangeToBBraun() { ChangePanelsDefault(OcclusionView.STAND_VIEW); }

    #endregion
}


// This enum marks which current panel it is currently on 
public enum OcclusionView
{
    OVERVIEW,
    TRAY,
    STAND_VIEW,

}

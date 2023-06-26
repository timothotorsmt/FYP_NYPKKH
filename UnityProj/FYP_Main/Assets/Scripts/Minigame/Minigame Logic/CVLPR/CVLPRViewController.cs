using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class controls the UI system of the minigame
public class CVLPRViewController : UIViewController<CVLPRView>
{

    // Override the run function
    protected override void Start()
    {
        base.Start();

        // Handle patient stuff here

        // Set up any base things at the same time
    }

    #region UI button callbacks
    


    #endregion
}


// This enum marks which current panel it is currently on 
public enum CVLPRView
{
    PATIENT_VIEW

}

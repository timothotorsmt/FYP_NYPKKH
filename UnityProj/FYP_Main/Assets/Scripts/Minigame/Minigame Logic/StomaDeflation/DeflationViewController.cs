using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class controls the UI system of the minigame
public class DeflationViewController : UIViewController<DeflationView>
{
    // Override the run function
    protected override void Start()
    {
        base.Start();

        // Handle patient stuff here

        // Set up any base things at the same time
    }

    #region UI button callbacks
    
    public void ChangeToPatient() 
    {
        ChangePanelsDefault(DeflationView.PATIENT_VIEW);

    }

    public void ChangeToOverview()
    {
        ChangePanelsDefault(DeflationView.OVERVIEW);

    }

    #endregion
}


// This enum marks which current panel it is currently on 
public enum DeflationView
{
    OVERVIEW,
    PATIENT_VIEW,
    STOMA_CLOSEUP,

}

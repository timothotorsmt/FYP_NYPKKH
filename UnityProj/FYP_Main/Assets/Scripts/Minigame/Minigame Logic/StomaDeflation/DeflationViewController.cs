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
    
    public void ChangeToPatient(int index) 
    {
        switch (index)
        {
            case 1:
                ChangePanelsDefault(DeflationView.PATIENT_VIEW_1);
                break;
            case 2:
                ChangePanelsDefault(DeflationView.PATIENT_VIEW_2);
                break;
            case 3:
                ChangePanelsDefault(DeflationView.PATIENT_VIEW_3);
                break;
        }
    }

    public void ChangeToOverview(int index)
    {
        switch (index)
        {
            case 1:
                ChangePanelsDefault(DeflationView.OVERVIEW_1);
                break;
            case 2:
                ChangePanelsDefault(DeflationView.OVERVIEW_1);
                break;
            case 3:
                ChangePanelsDefault(DeflationView.OVERVIEW_1);
                break;
        }
    }

    #endregion
}


// This enum marks which current panel it is currently on 
public enum DeflationView
{
    OVERVIEW_1,
    OVERVIEW_2,
    OVERVIEW_3,
    PATIENT_VIEW_1,
    PATIENT_VIEW_2,
    PATIENT_VIEW_3,
}

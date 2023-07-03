using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class controls the UI system of the minigame
public class PeripheralSetupViewController : UIViewController<PeripheralView>
{

    // Override the run function
    protected override void Start()
    {
        base.Start();

        // Handle patient stuff here

        // Set up any base things at the same time
    }

    #region UI button callbacks

    public void ChangetoTray() { ChangePanelsDefault(PeripheralView.TRAY); }
    public void ChangetoPatient() { ChangePanelsDefault(PeripheralView.PATIENT_VIEW); }
    public void ChangetoStand() { ChangePanelsDefault(PeripheralView.STAND_VIEW); }

    #endregion
}


// This enum marks which current panel it is currently on 
public enum PeripheralView
{
    OVERVIEW,
    TRAY,
    STAND_VIEW,
    PATIENT_VIEW
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UniRx;
using UniRx.Extention;
using Common.DesignPatterns;

// This class controls the UI system of the minigame
public class CVLItemUIController : UIViewController<CVLItemView>
{
    #region UI button callbacks
    public void ChangetoTray() { ChangePanelsDefault(CVLItemView.TRAY_DEFAULT); }
    public void ChangetoPatient() { ChangePanelsDefault(CVLItemView.PATIENT_VIEW); }
    public void ChangetoRollerSpikeCloseup() { ChangePanelsDefault(CVLItemView.PATIENT_VIEW); }
    #endregion
}


// This enum marks which current panel it is currently on 
public enum CVLItemView
{
    PATIENT_VIEW,
    TRAY_DEFAULT,
    TRAY,
}

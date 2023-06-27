using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UniRx;
using UniRx.Extention;
using Common.DesignPatterns;

// This class controls the UI system of the minigame
public class PeriLineSamUIController : UIViewController<PeriLineView>
{
    #region UI button callbacks
    public void ChangetoTray() { ChangePanelsDefault(PeriLineView.TRAY_DEFAULT); }
    public void ChangetoPatient() { ChangePanelsDefault(PeriLineView.PATIENT_VIEW); }
    public void ChangetoIVTubeCloseup() { ChangePanelsDefault(PeriLineView.IV_TUBE_CLOSEUP); }
    public void ChangetoRollerSpikeCloseup() { ChangePanelsDefault(PeriLineView.PATIENT_VIEW); }
    public void ChangetoIVBagWithSpikeCloseup() { ChangePanelsDefault(PeriLineView.IV_BAG_WITH_SPIKE_CLOSEUP); }
    #endregion
}


// This enum marks which current panel it is currently on 
public enum PeriLineView
{
    PATIENT_VIEW,
    TRAY_DEFAULT,
    IV_TUBE_CLOSEUP,
    TRAY,

    IV_BAG_CLOSEUP,
    IV_BAG_WITH_SPIKE_CLOSEUP,

}

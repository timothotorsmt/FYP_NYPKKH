using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UniRx;
using UniRx.Extention;
using Common.DesignPatterns;
using UnityEngine.Events;


// This class controls the UI system of the minigame
public class CVLItemUIController : UIViewController<CVLItemView>
{
    #region UI button callbacks

    public UnityEvent e;
    public void ChangetoTray() { ChangePanelsDefault(CVLItemView.TRAY_DEFAULT); }
    public void ChangetoPatient() { ChangePanelsDefault(CVLItemView.PATIENT_VIEW); }
    public void ChangetoSterileVideo() { ChangePanelsDefault(CVLItemView.STERILE_VIEW); }
    public void ChangetoDrapeVideo() { ChangePanelsDefault(CVLItemView.DRAPE_VIEW); }
    public void ChangetoDrapeOpen() { ChangePanelsDefault(CVLItemView.DRAPEOPEN_VIEW); }
    public void ChangetoItemOpen() { ChatGetter.Instance.StartChat("#CVLCAE", e); }

    public void ItemOpen() { ChangePanelsDefault(CVLItemView.ITEMOPEN_VIEW); }
    #endregion
}


// This enum marks which current panel it is currently on 
public enum CVLItemView
{
    PATIENT_VIEW,
    TRAY_DEFAULT,
    STERILE_VIEW,
    DRAPE_VIEW,
    DRAPEOPEN_VIEW,
    ITEMOPEN_VIEW,
}

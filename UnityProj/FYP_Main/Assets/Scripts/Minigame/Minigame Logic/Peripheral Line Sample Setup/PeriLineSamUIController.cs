using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UniRx;
using UniRx.Extention;
using Common.DesignPatterns;

// This class controls the UI system of the minigame
public class PeriLineSamUIController : MonoBehaviour
{
    // TODO: change this to a generic class LMAO
    #region UI variables
    // Patient stuff
    [SerializeField] private Image _patientSprite;

    // Panels
    private Stack<PeriLineView> _visitedPanels;
    private PeriLineView _currentPanel;
    [SerializeField] private List<PeriLinePanels> _viewPanels;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Init all other variables
        _visitedPanels = new Stack<PeriLineView>();
        _visitedPanels.Push(PeriLineView.PATIENT_VIEW);

        ChangePanels(_visitedPanels.Peek());

        // Generate a random patient
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Panel Changes
    private void ChangePanels(PeriLineView newView)
    {
        // Count if that panel even exists
        if (_viewPanels.Where(s => s.PanelView == newView).Count() == 0) 
        {
            Debug.LogWarning("The current panel " + newView.ToString() + " you are trying to access does not exist.");
            return;
        }
        
        // Check set the current panel to false
        var activePanels = _viewPanels.Where(s => s.PanelView == _currentPanel).Select(s => s.PanelObject);
        foreach (var Panel in activePanels)
        {
            Panel.SetActive(false);
        }

        // Set the last instance of the wanted panel to true
        _viewPanels.Where(s => s.PanelView == newView).Select(s => s.PanelObject).LastOrDefault().SetActive(true);

    }

    private void ChangePanelsDefault(PeriLineView newView)
    {
        ChangePanels(newView);  
        _visitedPanels.Push(newView);
        _currentPanel = newView;
    }

    public void Back()
    {
        _visitedPanels.Pop();
        Debug.Log(_visitedPanels.Peek());
        ChangePanels(_visitedPanels.Peek());
        _currentPanel = _visitedPanels.Peek();
    }
    #endregion

    #region UI button callbacks
    public void ChangetoTray() { ChangePanelsDefault(PeriLineView.TRAY_DEFAULT); }
    public void ChangetoPatient() { ChangePanelsDefault(PeriLineView.PATIENT_VIEW); }
    public void ChangetoIVTubeCloseup() { ChangePanelsDefault(PeriLineView.IV_TUBE_CLOSEUP); }
    public void ChangetoRollerSpikeCloseup() { ChangePanelsDefault(PeriLineView.PATIENT_VIEW); }
    public void ChangetoIVBagWithSpikeCloseup() { ChangePanelsDefault(PeriLineView.IV_BAG_WITH_SPIKE_CLOSEUP); }
    #endregion
}

// This class gives an object for the panels
[System.Serializable]
public class PeriLinePanels 
{
    public PeriLineView PanelView;
    public GameObject PanelObject;
}

// This enum marks which current panel it is currently on 
public enum PeriLineView
{
    PATIENT_VIEW,
    TRAY_DEFAULT,
    IV_TUBE_CLOSEUP,
    TRAY_,

    IV_BAG_CLOSEUP,
    IV_BAG_WITH_SPIKE_CLOSEUP,

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIViewController<ViewType> : MonoBehaviour where ViewType : struct, System.Enum
{
    #region UI variables
    // Patient stuff
    [SerializeField] private Image _patientSprite;

    // Panels
    private Stack<ViewType> _visitedPanels;
    private ViewType _currentPanel;
    [SerializeField] private List<ViewPanels<ViewType>> _viewPanels;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Init all other variables
        _visitedPanels = new Stack<ViewType>();
        _visitedPanels.Push((ViewType)(object)0);

        ChangePanels(_visitedPanels.Peek());

        // Generate a random patient
    }

    #region Panel Changes
    protected void ChangePanels(ViewType newView)
    {
        // Count if that panel even exists
        if (_viewPanels.Where(s => s.PanelView.ToString() == newView.ToString()).Count() == 0) 
        {
            Debug.LogWarning("The current panel " + newView.ToString() + " you are trying to access does not exist.");
            return;
        }
        
        // Check set the current panel to false
        var activePanels = _viewPanels.Where(s => s.PanelView.ToString() == _currentPanel.ToString()).Select(s => s.PanelObject);

        foreach (var Panel in activePanels)
        {
            Panel.SetActive(false);
        }

        // Set the last instance of the wanted panel to true
        _viewPanels.Where(s => s.PanelView.ToString() == newView.ToString()).Select(s => s.PanelObject).LastOrDefault().SetActive(true);

    }

    protected void ChangePanelsDefault(ViewType newView)
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
}

// This class gives an object for the panels
[System.Serializable]
public class ViewPanels<T> 
{
    public T PanelView;
    public GameObject PanelObject;
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Input;
using UnityEngine.Events;
using UnityEngine.UI;
using UniRx;
using Unity.VisualScripting;

// Allows for the dragging and "wiping" minigame mechanic
public class DragHover : MonoBehaviour, IInputActions
{
    #region hover variables

    [Header("Hover Variables")]
    [SerializeField] private GameObject _destination; // Destination of the gameobject
    [SerializeField, Min(0)] private float _hoverDuration = 3.0f; // How long the player should hover for
    [SerializeField, Min(0)] private float _hoverForgivance = 1.0f; // How far away do we accept the hover

    #endregion

    #region ui variables

    [Header("UI Variables")]
    [SerializeField] private Image _hoverSlider; 
    [SerializeField] private GameObject _sliderObject;
    [SerializeField] private GameObject _dragSign;
    [SerializeField] private UnityEvent _onDropOnDestination;
    [SerializeField] private UnityEvent _onReset;

    #endregion

    public bool _disable;
    private bool _isMoving;
    private bool _isFinished;

    private float _hoverTimer;

    private Vector2 _startPosition;
    private Vector2 _prevPosition;
    private Vector2 _resetPosition;
    private RectTransform _rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _resetPosition = _rectTransform.anchoredPosition;
        Reset();
    }

    // Reset all the values
    public void Reset()
    {
        // Make sure all the shadows r active
        _isFinished = false;
        UIInputManager.Instance.AddSubscriber(this);
        _sliderObject.SetActive(false);
        _rectTransform.anchoredPosition = _resetPosition;
        _hoverTimer = 0;

        if (_dragSign != null)
        {
            _dragSign.SetActive(true);
        }
        _onReset.Invoke();

    }

    void OnEnable()
    {
        if (UIInputManager.Instance != null)
        {
            UIInputManager.Instance.AddSubscriber(this);
        }
    }

    void OnDisable()
    {
        if (UIInputManager.Instance != null)
        {
            UIInputManager.Instance.RemoveSubscriber(this);
        }
    }

    public void SetDisable(bool newState)
    {
        if (newState)
        {
            _hoverSlider.gameObject.SetActive(false);
            _sliderObject.SetActive(false);
            _disable = true;
        }
        else 
        {
            _hoverSlider.gameObject.SetActive(true);
            _sliderObject.SetActive(true);
            _disable = false;
        }
    }

    public void OnStartTap()
    {
        // dc if its finished
        if (_isFinished) { return; }

        Vector2 mousePos = InputUtils.GetInputPosition();

        // Check if the distance between mouse and current item is less than 1
        // If yes, then start the dragging mechanic
        if (Vector2.Distance((Vector2)this.transform.position, mousePos) < 1.0f)
        {
            _startPosition = ((Vector2)this.transform.position - mousePos);
            _isMoving = true;

            if (_dragSign != null)
            {
                // Disable drag sign because it's kinda annoying
                // More of an aesthetic functionality tbvh
                _dragSign.SetActive(false);
            }
        }

        _prevPosition = _startPosition;

    }

    public void OnTap()
    {   
        // Move the item
        if (_isMoving)
        {
            Vector2 mousePos = (Vector2)InputUtils.GetInputPosition() + _startPosition;

            // If the item is moving at all
            // Note: epsilon is used to represent a very small number bc floats rarely will == 0
            if ((mousePos - _prevPosition).SqrMagnitude() > Mathf.Epsilon && !_disable)
            {
                // Check if distance between the current object and destination is acceptable
                if (Vector2.Distance((Vector2)mousePos, (Vector2)_destination.transform.position) < _hoverForgivance)
                {
                    _sliderObject.SetActive(true);

                    // Add timer only if 
                    // 1.) its moving
                    // 2.) its near enough to the destination
                    _hoverTimer += Time.deltaTime;
                    _hoverSlider.fillAmount = _hoverTimer / _hoverDuration;
                    if (_hoverTimer >= _hoverDuration)
                    {
                        _onDropOnDestination.Invoke();
                        _isFinished = true;
                    }
                }

            }

            _prevPosition = mousePos;

            this.transform.position = mousePos;
        }
    }

    public void OnEndTap()
    {
        _isMoving = false;
        _sliderObject.SetActive(false);

        if (_rectTransform) {
            // Set it back to its original position
            _rectTransform.anchoredPosition = _resetPosition;
        }
        if (_dragSign != null)
        {
            _dragSign.SetActive(true);
        }
    }
}

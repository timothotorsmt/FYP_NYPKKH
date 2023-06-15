using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Core.Input;

public class PlayerMovement : MonoBehaviour, IInputActions
{
    // PlayerMovement Variables
    private Vector3 _playerDestination;
    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rigidbody2D;

    
    [SerializeField, Min(0f)] private float _playerSpeed = 1.0f;
    [SerializeField, Range(0f, 1f)] private float _bottomForgivance = 0.9f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _playerDestination = Vector3.negativeInfinity;
    }

    void OnEnable()
    {
        InputManager.Instance.AddSubscriber(this);
    }

    void OnDisable()
    {
        if (InputManager.Instance != null) {
            InputManager.Instance.RemoveSubscriber(this);
        }
    }

    private void FixedUpdate()
    {
        if (!_playerDestination.Equals(Vector3.negativeInfinity))
        {
            // Check if there is a difference between the player current position and the ideal position
            if (Mathf.Abs((this.gameObject.transform.position - _playerDestination).sqrMagnitude) > Mathf.Epsilon) 
            {
                // Move the movement
                transform.position = Vector3.MoveTowards(transform.position, _playerDestination, _playerSpeed * Time.deltaTime);
            }
        }

        
    }

    public void OnStartTap()
    {
        _playerDestination = InputUtils.GetInputPosition();
        _playerDestination.y += Mathf.Abs(this.transform.position.y - _boxCollider2D.bounds.min.y) * _bottomForgivance;
        _playerDestination.z = this.transform.position.z;
    }

    public void OnTap()
    {

    }

    public void OnEndTap()
    {

    }
}

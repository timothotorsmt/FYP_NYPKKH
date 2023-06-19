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
                transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, _playerDestination.x, _playerSpeed * Time.deltaTime), transform.position.y, transform.position.z);
            }
        }

        
    }

    public void OnStartTap()
    {
        _playerDestination = InputUtils.GetInputPosition();

    }

    public void OnTap()
    {

    }

    public void OnEndTap()
    {

    }
}

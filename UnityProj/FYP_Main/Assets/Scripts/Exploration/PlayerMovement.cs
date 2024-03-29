using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Core.Input;
using UniRx.Extention;
using TMPro;

public class PlayerMovement : MonoBehaviour, IInputActions
{
    // PlayerMovement Variables
    public ReactiveProp<PlayerState> CurrentPlayerState;

    private Vector3 _playerDestination;
    private float _yOffset = 1f;
    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rigidbody2D;

    [SerializeField, Min(0f)] private float _playerSpeed = 1.0f;
    [SerializeField, Min(0f)] private float _playerForgivance = 0.05f;

    // Start is called before the first frame update
    public void Init()
    {
        CurrentPlayerState = new ReactiveProp<PlayerState>();

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _playerDestination = Vector3.negativeInfinity;

        InputManager.Instance.AddSubscriber(this);
    }

    private void FixedUpdate()
    {
        if (!_playerDestination.Equals(Vector3.negativeInfinity))
        {
            // Check if there is a difference between the player current position and the ideal position
            if (Mathf.Abs((this.gameObject.transform.position.x - _playerDestination.x)) > _playerForgivance) 
            {
                // Move the movement
                transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, _playerDestination.x, _playerSpeed * Time.deltaTime), transform.position.y, transform.position.z);
                CurrentPlayerState.SetValue(PlayerState.MOVING);
            }
            else 
            {
                CurrentPlayerState.SetValue(PlayerState.IDLE);
            }

        }
        else if (CurrentPlayerState.GetValue() == PlayerState.MOVING)
        {
            CurrentPlayerState.SetValue(PlayerState.IDLE);
        }
    }

    public void GoToDoor(Door _newDoor)
    {
        transform.position = new Vector3(_newDoor.GetSpawnPoint().x, _newDoor.GetSpawnPoint().y - _yOffset);
        _playerDestination = transform.position;
    }

    public void OnStartTap()
    {
        _playerDestination = InputUtils.GetInputPosition();
        if (_playerDestination.x < this.transform.position.x) 
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else 
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        
    }

    public void OnTap()
    {

    }

    public void OnEndTap()
    {

    }
}

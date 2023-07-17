using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Extention;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private Animator _playerAnimator;

    public void Init(PlayerMovement _playerMovement)
    {
        _playerAnimator = GetComponent<Animator>();
        _playerMovement.CurrentPlayerState.Value.Subscribe(state => {
            switch (state)
            {
                case PlayerState.IDLE:
                    _playerAnimator.SetInteger("PlayerMovementState", 0);
                    break;
                case PlayerState.MOVING:
                    _playerAnimator.SetInteger("PlayerMovementState", 1);
                    break;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

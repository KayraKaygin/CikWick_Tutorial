using System;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    [SerializeField] private Animator _playerAnimator;
    private StateController _stateController;

    private PlayerController _playerController;

    void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _stateController = GetComponent<StateController>();

    }

    void Start()
    {
        _playerController.OnPlayerJumped += PlayerController_OnPlayerJumped;
    }


    void Update()
    {
        SetPlayerAnimations();
    }

    private void SetPlayerAnimations()
    {
        var currentState = _stateController.GetCurrentState();

        switch (currentState)
        {
            case PlayerState.Idle:
                _playerAnimator.SetBool(Consts.PlayerAnimation.IS_SLIDING, false);
                _playerAnimator.SetBool(Consts.PlayerAnimation.IS_MOVING, false);
                break;

            case PlayerState.Move:
                _playerAnimator.SetBool(Consts.PlayerAnimation.IS_SLIDING, false);
                _playerAnimator.SetBool(Consts.PlayerAnimation.IS_MOVING, true);
                break;

            case PlayerState.SlideIdle:
                _playerAnimator.SetBool(Consts.PlayerAnimation.IS_SLIDING, true);
                _playerAnimator.SetBool(Consts.PlayerAnimation.IS_SLIDING_ACTIVE, false);
                break;
            case PlayerState.Slide:
                _playerAnimator.SetBool(Consts.PlayerAnimation.IS_SLIDING, true);
                _playerAnimator.SetBool(Consts.PlayerAnimation.IS_SLIDING_ACTIVE, true);
                break;
        }
    }


    private void PlayerController_OnPlayerJumped()
    {
        _playerAnimator.SetBool(Consts.PlayerAnimation.IS_JUMPING, true);
        Invoke(nameof(ResetJumping), 0.5f);
    }

    private void ResetJumping()
    {
        _playerAnimator.SetBool(Consts.PlayerAnimation.IS_JUMPING, false);
        
    }


}

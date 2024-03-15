using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, PlayerInput.IPlayerActions
{
    public event Action<Vector2> MovementEvent;
    public event Action<bool> JumpEvent; 

    public PlayerInput _playerActions;

    private void OnEnable()
    {
        if (_playerActions == null)
        {
            _playerActions = new PlayerInput();
            _playerActions.Player.SetCallbacks(this);
        }
        
        _playerActions.Player.Enable();  //Active Input
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 vec = context.ReadValue<Vector2>();
        MovementEvent?.Invoke(vec);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        JumpEvent?.Invoke(true);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, PlayerInput.IPlayerActions
{
    public event Action<Vector2> MovementEvent;
    public event Action<bool> MoveDownEvent;
    public event Action JumpEvent;
    public event Action<bool> BulletTimeEvent;
    public event Action<double> LightRush;

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

    public void OnMoveDown(InputAction.CallbackContext context)
    {
        if (context.performed)
            MoveDownEvent?.Invoke(true);
        else if (context.canceled)
            MoveDownEvent?.Invoke(false);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        JumpEvent?.Invoke();
    }

    public void OnBulletTime(InputAction.CallbackContext context)
    {
        if (context.performed)
            BulletTimeEvent?.Invoke(true);
        else if (context.canceled)
            BulletTimeEvent?.Invoke(false);
    }

    public void OnLightRush(InputAction.CallbackContext context)
    {
        double vec = context.ReadValue<Double>();
        LightRush?.Invoke(vec);
    }
}

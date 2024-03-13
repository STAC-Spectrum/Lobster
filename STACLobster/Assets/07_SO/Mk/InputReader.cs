using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, PlayerAcition.IPlayerActionActions
{
    public event Action<Vector2> MovementEvent;

    private PlayerAcition _playerActions;

    private void OnEnable()
    {
        if (_playerActions == null)
        {
            _playerActions = new PlayerAcition();
            _playerActions.PlayerAction.SetCallbacks(this);
        }
        
        _playerActions.Enable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 vec = context.ReadValue<Vector2>();
        MovementEvent?.Invoke(vec);
    }
    
}

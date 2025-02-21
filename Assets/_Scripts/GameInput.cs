using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get; private set;}

    private PlayerInputActiions inputActions;
    public event EventHandler OnMouse1;
    public event EventHandler OnEscapePressed;

    private void Awake()
    {
        Instance = this;
        inputActions = new PlayerInputActiions();
        inputActions.Gameplay.Enable();
    
        inputActions.Gameplay.Mouse1.performed += PlayerMouse1Performed;
        inputActions.Gameplay.Escape.performed += PlayerEscapePerformed;
    
    }

    private void PlayerEscapePerformed(InputAction.CallbackContext context)
    {
        OnEscapePressed?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerMouse1Performed(InputAction.CallbackContext context)
    {
        OnMouse1?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVector() {
        return inputActions.Gameplay.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMousePos() {
        return inputActions.Gameplay.MousePos.ReadValue<Vector2>();
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : Manager
{
    [SerializeField]
    private InputActionAsset inputActionAsset;
    
    [SerializeField]
    InputActionReference movement;
    
    [SerializeField]
    InputActionReference interaction;
    
    [SerializeField]
    InputActionReference camerazoom;
    
    public static event  Action<Vector2> OnMove;
    public static event  Action OnInteract;
    public static event  Action<float> OnZoom;
    
    public override void Init()
    {
    }

    public override void Enable()
    {
        inputActionAsset.Enable();
        movement.action.performed += StartMove;
        movement.action.canceled += StopMove;
        interaction.action.performed += Interact;
        camerazoom.action.performed += StartZoom;
        camerazoom.action.canceled += StopZoom;
    }

    public override void Disable()
    {
        inputActionAsset.Disable();
        movement.action.performed -= StartMove;
        movement.action.canceled -= StopMove;
        interaction.action.performed -= Interact;
        camerazoom.action.performed -= StartZoom;
        camerazoom.action.canceled -= StopZoom;
    }

    private void StartMove(InputAction.CallbackContext context)
    {
       var direction = context.ReadValue<Vector2>();
        
       OnMove?.Invoke(direction);
       
       
    }
    
    private void StartZoom(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<Vector2>();
        
        OnZoom?.Invoke(direction.y);
       
       
    }
    
    private void StopZoom(InputAction.CallbackContext context)
    {
    
        OnZoom?.Invoke(0);
       
       
    }
    
    private void StopMove(InputAction.CallbackContext context)
    {
        var direction = Vector2.zero;
        
        OnMove?.Invoke(direction);

    }

    private void Interact(InputAction.CallbackContext context)
    {
        OnInteract?.Invoke();
        
    }

}
using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : Manager
{
    [SerializeField] private InputActionAsset inputActionAsset;

    [SerializeField] InputActionReference clickmove;

    [SerializeField] InputActionReference interaction;

    [SerializeField] InputActionReference camerazoom;
    [SerializeField] InputActionReference cameraorbite;

    public static event Action OnMove;
    public static event Action OnInteract;
    public static event Action<float> OnZoom;
    public static event Action<float> OnOrbit;

    public override void Init()
    {
    }

    public override void Enable()
    {
        inputActionAsset.Enable();
        clickmove.action.performed += ClickMove;
        interaction.action.performed += Interact;
        camerazoom.action.performed += StartZoom;
        camerazoom.action.canceled += StopZoom;
        cameraorbite.action.performed += Orbit;
        cameraorbite.action.canceled += StopOrbit;
    }

    public override void Disable()
    {
        inputActionAsset.Disable();
        clickmove.action.performed -= ClickMove;
        interaction.action.performed -= Interact;
        camerazoom.action.performed -= StartZoom;
        camerazoom.action.canceled -= StopZoom;
        cameraorbite.action.performed -= Orbit;
        cameraorbite.action.canceled -= StopOrbit;
    }

    private void ClickMove(InputAction.CallbackContext context)
    {
        OnMove?.Invoke();
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

    private void Interact(InputAction.CallbackContext context)
    {
        OnInteract?.Invoke();
    }

    private void Orbit(InputAction.CallbackContext context)
    {
        var delta = context.ReadValue<Vector2>();
        OnOrbit?.Invoke(delta.x);
    }

    private void StopOrbit(InputAction.CallbackContext context)
    {
        OnOrbit?.Invoke(0f);
    }
}
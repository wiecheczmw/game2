// using System;
// using System.Collections.Generic; // Potrzebne do List<>
// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.UIElements; // UI Toolkit
//
// public class InputManager : Manager
// {
//     [Header("UI Reference")]
//     // Ważne: Jeśli to pole będzie puste, skrypt sam znajdzie UI w metodzie Init()
//     [SerializeField]
//     private UIDocument uiDocument;
//
//     [Header("Input Actions")] [SerializeField]
//     private InputActionAsset inputActionAsset;
//
//     [SerializeField] InputActionReference clickmove;
//     [SerializeField] InputActionReference interaction;
//     [SerializeField] InputActionReference camerazoom;
//     [SerializeField] InputActionReference cameraorbite;
//     
//     
//     // To jest ta flaga, o której mowa w tutorialu. 
//     // True = myszka jest nad przyciskiem. False = myszka jest nad grą.
//     private bool isPointerOverUI = false;
//
//     public static event Action OnMove;
//     public static event Action OnInteract;
//     public static event Action<float> OnZoom;
//     public static event Action<float> OnOrbit;
//
//     public override void Init()
//     {
//         // 1. Znajdź dokument UI, jeśli nie przypisałeś go ręcznie
//         if (uiDocument == null)
//         {
//             uiDocument = FindObjectOfType<UIDocument>();
//         }
//
//         if (uiDocument == null)
//         {
//             Debug.LogError("BŁĄD: Nie znaleziono UIDocument na scenie!");
//             return;
//         }
//
//         // 2. URUCHOM LOGIKĘ Z FILMIKU
//         RegisterUICallbacks();
//     }
//
//     private void RegisterUICallbacks()
//     {
//         var root = uiDocument.rootVisualElement;
//
//         // Metoda z filmiku:
//         // Szukamy wszystkich elementów, które mają blokować kliknięcia.
//         // Tutaj szukam wszystkich przycisków (Button).
//         List<VisualElement> elementsToBlock = new List<VisualElement>();
//
//         // Dodaj wszystkie przyciski do listy
//         root.Query().ForEach(btn => elementsToBlock.Add(btn));
//
//         // Opcjonalnie: Jeśli masz panele, które też mają blokować, możesz je dodać po klasie, np:
//         // root.Query(className: "menu-panel").ForEach(panel => elementsToBlock.Add(panel));
//
//         Debug.Log($"Zarejestrowano {elementsToBlock.Count} elementów UI blokujących ruch.");
//
//         // 3. Przypisanie zdarzeń (To jest sedno metody Samyam)
//         foreach (var element in elementsToBlock)
//         {
//             // Kiedy kursor wjeżdża na element
//             element.RegisterCallback<PointerEnterEvent>(evt =>
//             {
//                 isPointerOverUI = true;
//                 // Debug.Log("Mysz weszła na UI -> BLOKADA");
//             });
//
//             // Kiedy kursor zjeżdża z elementu
//             element.RegisterCallback<PointerOutEvent>(evt =>
//             {
//                 isPointerOverUI = false;
//                 // Debug.Log("Mysz wyszła z UI -> RUCH DOZWOLONY");
//             });
//         }
//     }
//
//     public override void Enable()
//     {
//         if (inputActionAsset != null) inputActionAsset.Enable();
//         if (clickmove != null) clickmove.action.performed += ClickMove;
//
//         // Reszta subskrypcji...
//         if (interaction != null) interaction.action.performed += Interact;
//         if (camerazoom != null) camerazoom.action.performed += StartZoom;
//         if (camerazoom != null) camerazoom.action.canceled += StopZoom;
//         if (cameraorbite != null) cameraorbite.action.performed += Orbit;
//         if (cameraorbite != null) cameraorbite.action.canceled += StopOrbit;
//     }
//
//     public override void Disable()
//     {
//         if (inputActionAsset != null) inputActionAsset.Disable();
//         if (clickmove != null) clickmove.action.performed -= ClickMove;
//
//         // Reszta odsubskrybowania...
//         if (interaction != null) interaction.action.performed -= Interact;
//         if (camerazoom != null) camerazoom.action.performed -= StartZoom;
//         if (camerazoom != null) camerazoom.action.canceled -= StopZoom;
//         if (cameraorbite != null) cameraorbite.action.performed -= Orbit;
//         if (cameraorbite != null) cameraorbite.action.canceled -= StopOrbit;
//     }
//
//     private void ClickMove(InputAction.CallbackContext context)
//     {
//         // Sprawdzenie zmiennej (tak jak na filmie)
//         if (isPointerOverUI)
//         {
//             // Jeśli prawda, nie robimy nic.
//             return;
//         }
//
//         OnMove?.Invoke();
//     }
//
//     // --- Reszta metod ---
//     private void StartZoom(InputAction.CallbackContext context)
//     {
//         OnZoom?.Invoke(context.ReadValue<Vector2>().y);
//     }
//
//     private void StopZoom(InputAction.CallbackContext context)
//     {
//         OnZoom?.Invoke(0);
//     }
//
//     private void Interact(InputAction.CallbackContext context)
//     {
//         if (isPointerOverUI) return; // Tu też warto zablokować
//         OnInteract?.Invoke();
//     }
//
//     private void Orbit(InputAction.CallbackContext context)
//     {
//         OnOrbit?.Invoke(context.ReadValue<Vector2>().x);
//     }
//
//     private void StopOrbit(InputAction.CallbackContext context)
//     {
//         OnOrbit?.Invoke(0f);
//     }
// }

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements; // UI Toolkit
using UnityEngine.EventSystems;

public class InputManager : Manager
{
    [Header("UI Reference")]
    [SerializeField] private UIDocument uiDocument;

    [Header("Input Actions")] 
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
        // 1. Znajdź dokument UI
        if (uiDocument == null)
        {
            // Nowsza wersja FindObjectOfType
            uiDocument = FindFirstObjectByType<UIDocument>();
        }

        if (uiDocument == null)
        {
            Debug.LogError("BŁĄD: Nie znaleziono UIDocument na scenie!");
            return;
        }
        
        // ZMIANA: Nie rejestrujemy już żadnych callbacków na starcie!
        // Dzięki temu system działa dla nowych okien otwieranych w trakcie gry.
    }

    // --- NOWA METODA: SERCE SYSTEMU ---
    // Sprawdza "na żywo", czy pod myszką jest element UI
    private bool IsPointerOverUI()
    {
        // Teraz to zadziała, bo dodałeś EventSystem na scenę
        if (EventSystem.current == null) return false;

        return EventSystem.current.IsPointerOverGameObject();
    }
    public override void Enable()
    {
        if (inputActionAsset != null) inputActionAsset.Enable();
        
        if (clickmove != null) clickmove.action.performed += ClickMove;
        if (interaction != null) interaction.action.performed += Interact;
        if (camerazoom != null) camerazoom.action.performed += StartZoom;
        if (camerazoom != null) camerazoom.action.canceled += StopZoom;
        if (cameraorbite != null) cameraorbite.action.performed += Orbit;
        if (cameraorbite != null) cameraorbite.action.canceled += StopOrbit;
    }

    public override void Disable()
    {
        if (inputActionAsset != null) inputActionAsset.Disable();
        
        if (clickmove != null) clickmove.action.performed -= ClickMove;
        if (interaction != null) interaction.action.performed -= Interact;
        if (camerazoom != null) camerazoom.action.performed -= StartZoom;
        if (camerazoom != null) camerazoom.action.canceled -= StopZoom;
        if (cameraorbite != null) cameraorbite.action.performed -= Orbit;
        if (cameraorbite != null) cameraorbite.action.canceled -= StopOrbit;
    }

    private void ClickMove(InputAction.CallbackContext context)
    {
        // Sprawdzamy stan w momencie kliknięcia
        if (IsPointerOverUI())
        {
            // Debug.Log("Zablokowano ruch - kursor nad UI");
            return;
        }

        OnMove?.Invoke();
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (IsPointerOverUI()) return;
        OnInteract?.Invoke();
    }

    // --- Reszta bez zmian ---
    private void StartZoom(InputAction.CallbackContext context)
    {
        OnZoom?.Invoke(context.ReadValue<Vector2>().y);
    }

    private void StopZoom(InputAction.CallbackContext context)
    {
        OnZoom?.Invoke(0);
    }

    private void Orbit(InputAction.CallbackContext context)
    {
        OnOrbit?.Invoke(context.ReadValue<Vector2>().x);
    }

    private void StopOrbit(InputAction.CallbackContext context)
    {
        OnOrbit?.Invoke(0f);
    }
}
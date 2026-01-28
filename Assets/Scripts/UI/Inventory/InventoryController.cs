using UnityEngine;
using UnityEngine.UIElements;

public class InventoryController
{
    private VisualElement _inventoryWindow;
    private Button _closeButton;

    // Zmienne do obsługi przesuwania
    private bool _isDragging = false;
    private Vector2 _startMousePosition;
    private Vector2 _startWindowPosition;

    public InventoryController(VisualElement root)
    {
        var inventoryInstance = root.Q<VisualElement>("InventoryInstance");
        Debug.Log(inventoryInstance);
        if (inventoryInstance != null)
        {
           
            _inventoryWindow = inventoryInstance.Q<VisualElement>("InventoryWindow");
            Debug.Log(_inventoryWindow);
            _closeButton = inventoryInstance.Q<Button>("CloseButton");

            // 1. Rejestracja przycisku zamknij
            if (_closeButton != null)
            {
                _closeButton.RegisterCallback<ClickEvent>(evt => CloseInventory());
            }

            // 2. Rejestracja przesuwania (Drag & Drop)
            if (_inventoryWindow != null)
            {
                // Ważne: Rejestrujemy zdarzenia na oknie, które chcemy przesuwać
                _inventoryWindow.RegisterCallback<PointerDownEvent>(OnPointerDown);
                _inventoryWindow.RegisterCallback<PointerMoveEvent>(OnPointerMove);
                _inventoryWindow.RegisterCallback<PointerUpEvent>(OnPointerUp);
            }
        }
    }

    // --- LOGIKA PRZESUWANIA (DRAG) ---

    private void OnPointerDown(PointerDownEvent evt)
    {
        // Sprawdzamy, czy kliknięto lewym przyciskiem
        if (evt.button != 0) return;

        _isDragging = true;
        
        // Pobieramy startową pozycję myszki
        _startMousePosition = evt.position;
        
        // Pobieramy obecną pozycję okna (z layoutu)
        _startWindowPosition = new Vector2(
            _inventoryWindow.resolvedStyle.left, 
            _inventoryWindow.resolvedStyle.top
        );

        // KLUCZOWE: "Łapiemy" kursor. Dzięki temu, nawet jak wyjedziesz myszką poza okno,
        // nadal będziesz je przesuwać, dopóki nie puścisz przycisku.
        _inventoryWindow.CapturePointer(evt.pointerId);
        
        // Zatrzymujemy propagację, żeby nie klikać w rzeczy pod spodem
        evt.StopPropagation();
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        if (!_isDragging) return;
        if (!_inventoryWindow.HasPointerCapture(evt.pointerId)) return;

        // Obliczamy różnicę (delta) ruchu myszki
        Vector2 currentMousePosition = evt.position;
        Vector2 delta = currentMousePosition - _startMousePosition;

        // Obliczamy nową pozycję okna
        float newLeft = _startWindowPosition.x + delta.x;
        float newTop = _startWindowPosition.y + delta.y;

        // Aktualizujemy style UI
        _inventoryWindow.style.left = newLeft;
        _inventoryWindow.style.top = newTop;
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        if (!_isDragging) return;
        if (evt.button != 0) return;

        _isDragging = false;
        
        // Puszczamy kursor
        _inventoryWindow.ReleasePointer(evt.pointerId);
        evt.StopPropagation();
    }

    // --- LOGIKA OTWIERANIA/ZAMYKANIA (Bez zmian) ---

    public void OpenInventory()
    {
        if (_inventoryWindow != null)
        {
            _inventoryWindow.style.display = DisplayStyle.Flex;
            // Opcjonalnie: Przesuń na wierzch (jeśli masz więcej okien)
            _inventoryWindow.BringToFront(); 
        }
    }

    public void CloseInventory()
    {
        if (_inventoryWindow != null)
        {
            _inventoryWindow.style.display = DisplayStyle.None;
        }
    }

    public void ToggleInventory()
    {
        if (_inventoryWindow == null) return;
        bool isVisible = _inventoryWindow.style.display == DisplayStyle.Flex;
        
        if (isVisible) CloseInventory();
        else OpenInventory();
    }
}
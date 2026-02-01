using UnityEngine;
using UnityEngine.UIElements;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private VisualTreeAsset slotTemplate; // Opcjonalnie: jeśli masz prefab slotu w UXML

    private VisualElement _inventoryWindow;
    private VisualElement _header;
    private Button _closeButton;
    private VisualElement _slotsContainer; // Nowe pole na kontener slotów

    // --- DRAG STATE ---
    private bool _isDragging = false;
    private Vector2 _startMousePosition;
    private Vector2 _startWindowPosition;
    private VisualElement _inventoryLayer;
    private void OnEnable() // Zmieniam na OnEnable, często bezpieczniejsze dla UI niż Awake
    {
        var root = uiDocument.rootVisualElement;

        // Jeśli masz wrapper "InventoryInstance", odkomentuj linię poniżej:
        // var inventoryInstance = root.Q<VisualElement>("InventoryInstance");
        // Jeśli nie, szukamy bezpośrednio w root (dla uproszczenia przykładu):
        var container = root; 
        
        _inventoryLayer = root.Q<VisualElement>("Inventory");
        _inventoryWindow = container.Q<VisualElement>("InventoryWindow");
        _header = _inventoryWindow.Q<VisualElement>("Header"); // To będzie cały pasek nagłówka
        _closeButton = _inventoryWindow.Q<Button>("CloseButton");
        _slotsContainer = _inventoryWindow.Q<VisualElement>("SlotsContainer"); // Szukamy kontenera na sloty

        // --- GENEROWANIE 48 SLOTÓW ---
        if (_slotsContainer != null)
        {
            _slotsContainer.Clear(); // Czyścimy na wszelki wypadek
            for (int i = 0; i < 48; i++)
            {
                VisualElement slot = new VisualElement();
                slot.AddToClassList("slot"); // Klasa z USS
                slot.name = $"Slot_{i}";
                
                // Opcjonalnie: tekst dla debugu
                // slot.Add(new Label($"{i+1}")); 
                
                _slotsContainer.Add(slot);
            }
        }

        // --- OBSŁUGA ZDARZEŃ ---
        if (_closeButton != null)
            _closeButton.RegisterCallback<ClickEvent>(_ => CloseInventory());

        if (_header != null)
        {
            _header.RegisterCallback<PointerDownEvent>(OnPointerDown);
            _header.RegisterCallback<PointerMoveEvent>(OnPointerMove);
            _header.RegisterCallback<PointerUpEvent>(OnPointerUp);
        }
    }

    // ---------- LOGIKA PRZESUWANIA (Twoja, bez zmian) ----------

    private void OnPointerDown(PointerDownEvent evt)
    {
        if (evt.button != 0) return;
        if (_inventoryLayer != null)
        {
            _inventoryLayer.BringToFront();
        }
        _isDragging = true;
        _startMousePosition = evt.position;

        _startWindowPosition = new Vector2(
            _inventoryWindow.resolvedStyle.left,
            _inventoryWindow.resolvedStyle.top
        );

        _header.CapturePointer(evt.pointerId);
        evt.StopPropagation();
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        if (!_isDragging) return;
        if (!_header.HasPointerCapture(evt.pointerId)) return;

        Vector2 currentMousePosition = evt.position;
        Vector2 delta = currentMousePosition - _startMousePosition;

        // Używamy style.left/top zamiast transform dla layoutu absolutnego
        _inventoryWindow.style.left = _startWindowPosition.x + delta.x;
        _inventoryWindow.style.top = _startWindowPosition.y + delta.y;
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        if (!_isDragging) return;
        if (evt.button != 0) return;

        _isDragging = false;
        _header.ReleasePointer(evt.pointerId);
        evt.StopPropagation();
    }

    public void CloseInventory()
    {
        if (_inventoryWindow != null)
            _inventoryWindow.style.display = DisplayStyle.None;
    }
}
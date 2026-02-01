using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem; 


public class PlayerInfoController : MonoBehaviour
{

    [SerializeField] private UIDocument uiDocument;
    
    private VisualElement _playerInfoWindow;
    private VisualElement _header;
    private Button _closeButton;

    // --- DRAG STATE ---
    private bool _isDragging = false;
    private Vector2 _startMousePosition;
    private Vector2 _startWindowPosition;
    private VisualElement _playerInfoLayer;
    private void OnEnable() // Zmieniam na OnEnable, często bezpieczniejsze dla UI niż Awake
    {
        var root = uiDocument.rootVisualElement;
        
        var container = root; 
        _playerInfoLayer = root.Q<VisualElement>("PlayerInfo");
        _playerInfoWindow = container.Q<VisualElement>("PlayerInfoWindow");
        Debug.Log(_playerInfoWindow.name);
        _header = _playerInfoWindow.Q<VisualElement>("Header"); // To będzie cały pasek nagłówka
        Debug.Log(_header.name);
        _closeButton = _playerInfoWindow.Q<Button>("CloseButton");
        Debug.Log(_closeButton);

        
        
        // --- OBSŁUGA ZDARZEŃ ---
        if (_closeButton != null)
            _closeButton.RegisterCallback<ClickEvent>(_ => ClosePlayerInfo());

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
        if (_playerInfoLayer != null)
        {
            _playerInfoLayer.BringToFront();
        }
        _isDragging = true;
        _startMousePosition = evt.position;

        _startWindowPosition = new Vector2(
            _playerInfoWindow.resolvedStyle.left,
            _playerInfoWindow.resolvedStyle.top
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
        _playerInfoWindow.style.left = _startWindowPosition.x + delta.x;
        _playerInfoWindow.style.top = _startWindowPosition.y + delta.y;
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        if (!_isDragging) return;
        if (evt.button != 0) return;

        _isDragging = false;
        _header.ReleasePointer(evt.pointerId);
        evt.StopPropagation();
    }

    public void ClosePlayerInfo()
    {
        Debug.Log("ClosePlayerInfo");
        if (_playerInfoWindow != null)
            _playerInfoWindow.style.display = DisplayStyle.None;
    }


}

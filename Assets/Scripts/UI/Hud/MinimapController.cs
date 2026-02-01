using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem; 


public class MinimapController : MonoBehaviour
{

    [SerializeField] private UIDocument uiDocument;

    private VisualElement _inventoryWindow;
    private VisualElement _inventoryLayer;
    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;
        var inventoryInstance = root.Q<VisualElement>("InventoryInstance");
        _inventoryLayer = root.Q<VisualElement>("Inventory");
        _inventoryWindow = inventoryInstance.Q<VisualElement>("InventoryWindow");
        
        
        
        var minimap = root.Q<VisualElement>("MinimapInstance");
        if (minimap != null)
        {
            minimap.RegisterCallback<ClickEvent>(evt =>
            {
                // Debug.Log("Kliknięto minimapę!");
                OpenInventory();
            });
        }
    }

    public void OpenInventory()
    {
        if (_inventoryWindow != null)
        {
            if (_inventoryLayer != null)
            {
                _inventoryLayer.BringToFront();
            }
            _inventoryWindow.style.display = DisplayStyle.Flex;
            // Opcjonalnie: Przesuń na wierzch (jeśli masz więcej okien)
            _inventoryWindow.BringToFront(); 
        }
    }
    
    private void Awake()
    {
      
 
    }

}

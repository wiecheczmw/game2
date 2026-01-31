using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem; 


public class MinimapController : MonoBehaviour
{

    [SerializeField] private UIDocument uiDocument;

    private VisualElement _inventoryWindow;
    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;
        var inventoryInstance = root.Q<VisualElement>("InventoryInstance");
        
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
            _inventoryWindow.style.display = DisplayStyle.Flex;
            // Opcjonalnie: Przesuń na wierzch (jeśli masz więcej okien)
            _inventoryWindow.BringToFront(); 
        }
    }
    
    private void Awake()
    {
      
 
    }

}

// using UnityEngine;
// using UnityEngine.UIElements;
// using UnityEngine.InputSystem; // Pamiętaj o tym, jeśli używasz Keyboard.current
//
// public class UIManager : Manager
// {
//     [SerializeField] private UIDocument uiDocument;
//
//     public HUDController HUD { get; private set; }
//     public InventoryController Inventory { get; private set; }
//
//
//     // ZMIANA: Używamy OnEnable, bo to Unity uruchamia automatycznie!
//     private void OnEnable()
//     {
//         if (uiDocument == null) uiDocument = GetComponent<UIDocument>();
//
//         if (uiDocument == null)
//         {
//             Debug.LogError("Brak UIDocument!");
//             return;
//         }
//
//         var root = uiDocument.rootVisualElement;
//
//         // 1. Tworzymy kontrolery
//         HUD = new HUDController(root);
//         Inventory = new InventoryController(root);
//
//         // 2. Podpinamy minimapę (To jest ten fragment, który wcześniej nie działał)
//         var minimap = root.Q<VisualElement>("MinimapInstance");
//         if (minimap != null)
//         {
//             minimap.RegisterCallback<ClickEvent>(evt => 
//             {
//                 // Debug.Log("Kliknięto minimapę!");
//                 Inventory.OpenInventory();
//             });
//         }
//         else
//         {
//             Debug.LogWarning("Nie znaleziono MinimapInstance - sprawdź nazwę w UXML!");
//         }
//
//         // 3. Dane startowe
//         HUD.SetPlayerInfo("Hero", 33);
//         HUD.UpdateHealth(1754, 2000);
//     }
//
//     // Klasa Manager wymaga metody Init, więc zostawiamy ją pustą (lub przenieś tam logikę, jeśli masz GameManager)
//     public override void Init() { }
//     
//     public override void Enable() { }
//     public override void Disable() { }
//
//     void Update()
//     {
//         // Obsługa klawisza "I" bez zmian w InputManagerze
//         if (Keyboard.current != null && Keyboard.current.iKey.wasPressedThisFrame)
//         {
//             Inventory.ToggleInventory();
//         }
//         // Obsługa klawisza "c" 
//         // if (Keyboard.current != null && Keyboard.current.cKey.wasPressedThisFrame)
//         // {
//         //     PlayerFrame?.ToggleWindow();
//         // }
//         
//     }
//     
//     
//
// }
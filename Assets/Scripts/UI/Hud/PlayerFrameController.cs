using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem; 


public class PlayerFrameController : MonoBehaviour
{

   
    [SerializeField] private UIDocument uiDocument;

    private VisualElement _PlayerInfoWindow;
    private VisualElement _playerInfoLayer;
    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;

        _playerInfoLayer = root.Q<VisualElement>("PlayerInfo");
        _PlayerInfoWindow = root.Q<VisualElement>("PlayerInfoWindow");
        
        var playerframe = root.Q<VisualElement>("PlayerInstance");
        Debug.Log(playerframe);
        if (playerframe != null)
        {
            playerframe.RegisterCallback<ClickEvent>(evt =>
            {
                // Debug.Log("Kliknięto player frame !");
                OpenPlayerInfo();
            });
        }
    }
    


    public void OpenPlayerInfo()
    {
        Debug.Log("Open PlayerInfo log");
        if (_PlayerInfoWindow != null)
        {
            if (_playerInfoLayer != null)
            {
                _playerInfoLayer.BringToFront();
            }
           // Debug.Log("jest ");
            _PlayerInfoWindow.style.display = DisplayStyle.Flex;
            // Opcjonalnie: Przesuń na wierzch (jeśli masz więcej okien)
            _PlayerInfoWindow.BringToFront(); 
        }
    }
    
    private void Awake()
    {
      
 
    }



}

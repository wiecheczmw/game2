using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem; 


public class PlayerFrameController : MonoBehaviour
{

   
    [SerializeField] private UIDocument uiDocument;

    private VisualElement _PlayerInfoWindow;
    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;

        _PlayerInfoWindow = root.Q<VisualElement>("PlayerInfoWindow");
        
        var playerframe = root.Q<VisualElement>("PlayerInstance");
        Debug.Log(playerframe);
        if (playerframe != null)
        {
            playerframe.RegisterCallback<ClickEvent>(evt =>
            {
                 Debug.Log("Kliknięto player frame !");
                OpenPlayerInfo();
            });
        }
    }
    


    public void OpenPlayerInfo()
    {
        Debug.Log("Open PlayerInfo log");
        if (_PlayerInfoWindow != null)
        {
            Debug.Log("jest ");
            _PlayerInfoWindow.style.display = DisplayStyle.Flex;
            _PlayerInfoWindow.style.height = 100;
            // Opcjonalnie: Przesuń na wierzch (jeśli masz więcej okien)
            _PlayerInfoWindow.BringToFront(); 
        }
    }
    
    private void Awake()
    {
      
 
    }



}

using System;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] RichAI richAI;
    

    private void OnEnable()
    {
        InputManager.OnMove += clickmove;
        InputManager.OnInteract += Interact;
    }

    private void OnDisable()
    {
        InputManager.OnMove -= clickmove;
        InputManager.OnInteract -= Interact;
    }
    // public bool IsPointerOverUI()
    // {
    //     var panel = UnityEngine.UIElements.RuntimePanelUtils
    //         .ScreenToPanel(UIDocument.panel, Input.mousePosition);
    //
    //     return panel != null;
    // }
    private void clickmove()
    {
        
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition), out RaycastHit hit))
        {
           // richAI.position.Set(hit.point.x, hit.point.y, hit.point.z);
            richAI.destination = hit.point;
        }
        
    }
    
    
    private void Update()
    {
      
    }

    private void Move(Vector2 direction)
    {
    
    }


    private void Interact()
    {
        Debug.Log("Interact");
    }
}
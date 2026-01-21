using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private CharacterController controller;

    private Vector3 movementdirection;
  

    private void OnEnable()
    {
     InputManager.OnMove += Move;   
     InputManager.OnInteract += Interact;
    }
    private void OnDisable()
    {
        InputManager.OnMove -= Move;   
        InputManager.OnInteract -= Interact;
    }

    private void Update()
    {
        controller.SimpleMove(movementdirection);
    }

    private void Move(Vector2 direction)
    {
        movementdirection= new Vector3(direction.x, 0, direction.y);
        
    }

    private void Interact()
    {
        Debug.Log("Interact");
        
    }
}

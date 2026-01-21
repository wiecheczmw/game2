using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    private Vector3 movementdirection;
    private float rotationSpeed = 10f;

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
        if (movementdirection != Vector3.zero)
        {
            // Oblicz docelową rotację na podstawie wektora ruchu
            Quaternion targetRotation = Quaternion.LookRotation(movementdirection);

            // Płynnie obróć postać w stronę docelową (Slerp)
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        controller.SimpleMove(movementdirection);
    }

    private void Move(Vector2 direction)
    {
        movementdirection = new Vector3(direction.x, 0, direction.y);
    }


    private void Interact()
    {
        Debug.Log("Interact");
    }
}
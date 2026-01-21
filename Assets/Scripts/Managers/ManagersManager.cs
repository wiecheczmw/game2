using System;
using Unity.VisualScripting;
using UnityEngine;

public class ManagersManager : MonoBehaviour
{
    [SerializeField] private Manager[] managers;

    void Awake()
    {
        foreach (var manager in managers)
        {
            manager.Init();
        }
    }

    private void OnEnable()
    {
        foreach (var manager in managers)
        {
            manager.Enable();
        }
    }

    private void OnDisable()
    {
        foreach (var manager in managers)
        {
            manager.Disable();
        }
    }
}
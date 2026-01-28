using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem; 

public class ActionBarController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    
    private void Awake()
    {
      
     uiDocument.rootVisualElement.Q<Button>("button1").clicked += addtestmovespeed;
    }

    private void addtestmovespeed()
    {
        FindAnyObjectByType<CharacterStatistics>(FindObjectsInactive.Exclude).MovementSpeed.Addpercentmodifier(10);
    }
}

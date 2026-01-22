using System;
using UnityEngine;
using UnityEngine.UIElements;

public class testUI : MonoBehaviour
{
    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var button= root.Q<Button>("testbutton");
        
        button.clicked += ButtonOnclicked;
        
    }

    private void ButtonOnclicked()
    {
       Debug.Log("ButtonOnclicked");
    }
}

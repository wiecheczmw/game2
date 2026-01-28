using System;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Statistic
{
    [SerializeField]
    private float baseValue;

    private bool isinited;
    private List<float> percentmodifiers=new();
    private List<float> flatmodifiers=new();
    private float currentvalue;

    public float Value
    {
        get
        {
           if (!isinited)
               {
                CalculateCurrnetValue();
               }
           return currentvalue;
        }
    }

    public event Action OnChange;

    public void Addpercentmodifier(float value)
    {
        percentmodifiers.Add(value);
        CalculateCurrnetValue();
    }

    public void Addflatmodifier(float value)
    {
        flatmodifiers.Add(value);
        CalculateCurrnetValue();
    }
    
    
    private void CalculateCurrnetValue()
    {
        isinited=true;
        
        var precentValue = 100f;
        
        foreach (var modifier in percentmodifiers)
            {
                precentValue += modifier;
            }
        var flatValue = 0f;
        
        foreach (var modifier in flatmodifiers){
            flatValue += modifier;
            
        }

        currentvalue = baseValue;
        
        currentvalue += (baseValue*precentValue*0.01f);
        currentvalue += flatValue;
        OnChange?.Invoke();
    }
    
}

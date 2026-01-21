using System;
using UnityEngine;

public abstract class Manager : MonoBehaviour
{
    public abstract void Init();

    public abstract void Enable();

    public abstract void Disable();
}
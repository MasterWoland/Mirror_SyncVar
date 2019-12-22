using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static event Action OnToggleMoveCameraOnClient;
    
    public void ToggleMoveCameraOnClient()
    {
        OnToggleMoveCameraOnClient?.Invoke();
    }
}

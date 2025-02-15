using System;
using UnityEngine;

/// <summary>
/// A controlling class that only exists within a scene's scope.
/// </summary>
public abstract class Controller : MonoBehaviour
{
    protected abstract bool RegisterToManager();
    protected abstract bool UnregisterFromManager();
    
    protected virtual void OnEnable()
    {
        if (!RegisterToManager())
        {
            Debug.LogWarning($"Failed to register controller {GetType().Name}.");
        }
    }
    
    protected virtual void OnDisable()
    {
        if (!UnregisterFromManager())
        {
            Debug.LogWarning($"Failed to unregister controller {GetType().Name}.");
        }
    }
}

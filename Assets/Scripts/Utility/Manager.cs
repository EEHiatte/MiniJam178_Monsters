using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An overarching manager that can contain several controllers.
/// The manager exists indefinitely while controllers are scene-specific.
/// </summary>
/// <typeparam name="T">Type of manager</typeparam>
public class Manager<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly Dictionary<Type, object> controllers = new Dictionary<Type, object>();
    
    public static T Instance => _instance;
    
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public bool RegisterController<TController>(TController controller) where TController : Controller
    {
        var type = typeof(TController);
        if (!controllers.TryAdd(type, controller))
        {
            Debug.LogWarning($"Controller of type {type} already exists.");
            return false;
        }

        return true;
    }
    
    public bool UnregisterController<TController>(TController controller) where TController : Controller
    {
        var type = typeof(TController);
        if (controllers.ContainsKey(type))
        {
            controllers.Remove(type);
            return true;
        }
        Debug.LogWarning($"Controller of type {type} not found.");
        return false;
    }
    
    public TController GetController<TController>() where TController : Controller
    {
        var type = typeof(TController);
        if (controllers.TryGetValue(type, out var controller))
        {
            return controller as TController;
        }
        Debug.LogWarning($"Controller of type {type} not found.");
        return null;
    }
}

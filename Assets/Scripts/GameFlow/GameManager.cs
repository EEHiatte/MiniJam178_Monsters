using UnityEngine;

/// <summary>
///     The main game manager that can have controllers registered and unregistered through gameplay.
///     Provides a central point of access for game logic and flow.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
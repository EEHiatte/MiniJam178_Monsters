using UnityEngine;

/// <summary>
/// Controller for a level's handling.
/// </summary>
public class LevelController : Controller 
{
    // TODO: Contain level-specific things here
    // Like something that handles waves/enemy spawning
    // Current player gold, etc
    [SerializeField]
    private Path path;
    
    public Path Path => path;
    
    protected override bool RegisterToManager()
    {
        return GameManager.Instance.RegisterController(this);
    }

    protected override bool UnregisterFromManager()
    {
        return GameManager.Instance.UnregisterController(this);
    }
}

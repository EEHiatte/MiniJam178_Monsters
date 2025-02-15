using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class that controls the main menu.
/// </summary>
public class MainMenuController : Controller
{
    // TODO: Store main menu buttons and other related objects and behavior.
    [SerializeField] private Button playButton;
    
    [SerializeField] private Button quitButton;
    
    protected override bool RegisterToManager()
    {
        if(GameManager.Instance.RegisterController(this))
        {
            RegisterMenuButtons();
            return true;
        }
        
        return false;
    }

    protected override bool UnregisterFromManager()
    {
        UnregisterMenuButtons();
        return GameManager.Instance.UnregisterController(this);
    }
    
    private void RegisterMenuButtons()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }
    
    private void UnregisterMenuButtons()
    {
        playButton.onClick.RemoveListener(OnPlayButtonClicked);
        quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }
    
    private void OnPlayButtonClicked()
    {
        // TODO: Load the level scene
    }

    private void OnQuitButtonClicked()
    {
        // TODO: Quit game through GameManager
    }
}

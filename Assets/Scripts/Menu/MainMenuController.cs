using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class that controls the main menu.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    // TODO: Store main menu buttons and other related objects and behavior.
    [SerializeField] private Button playButton;
    
    [SerializeField] private Button quitButton;
    
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

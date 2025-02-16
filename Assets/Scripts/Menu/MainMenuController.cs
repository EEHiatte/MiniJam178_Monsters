using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class that controls the main menu.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    // TODO: Store main menu buttons and other related objects and behavior.
    [SerializeField] private Button playButton;
    
    [SerializeField] private Button quitButton;

    [SerializeField] private Button Level1;

    [SerializeField] private Button Level2;

    [SerializeField] private Button Level3;

    [SerializeField] private Button backButton;

    private void Start()
    {
        RegisterMenuButtons();
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
        UnregisterMenuButtons();
        playButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        Level1.gameObject.SetActive(true);
        Level2.gameObject.SetActive(true);
        Level3.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
        backButton.onClick.AddListener(OnBackButtonClicked);

        // TODO: Load the level scene
        if (PlayerPrefs.HasKey("CompletedLevel")) 
        {
            switch (PlayerPrefs.GetInt("CompletedLevel"))
            {
                case 0:
                    Level1.enabled = true;
                    Level1.onClick.AddListener(OnLevel1ButtonClicked);
                    Level2.enabled = false;
                    Level3.enabled = false;
                    break;
                case 1:
                    Level1.enabled = true;
                    Level1.onClick.AddListener(OnLevel1ButtonClicked);
                    Level2.enabled = true;
                    Level2.onClick.AddListener(OnLevel2ButtonClicked);
                    Level3.enabled = false;
                    break;
                case 2:
                    Level1.enabled = true;
                    Level1.onClick.AddListener(OnLevel1ButtonClicked);
                    Level2.enabled = true;
                    Level2.onClick.AddListener(OnLevel2ButtonClicked);
                    Level3.enabled = true;
                    Level3.onClick.AddListener(OnLevel3ButtonClicked);
                    break;
            }
        }
        else
        {
            Level1.enabled = true;
            Level1.onClick.AddListener(OnLevel1ButtonClicked);
        }
    }

    private void OnLevel1ButtonClicked()
    {
        Level1.onClick.RemoveAllListeners();
        Level2.onClick.RemoveAllListeners();
        Level3.onClick.RemoveAllListeners();
        LoadLevel(0);
    }

    private void OnLevel2ButtonClicked()
    {
        Level1.onClick.RemoveAllListeners();
        Level2.onClick.RemoveAllListeners();
        Level3.onClick.RemoveAllListeners();
        LoadLevel(1);
    }

    private void OnLevel3ButtonClicked()
    {
        Level1.onClick.RemoveAllListeners();
        Level2.onClick.RemoveAllListeners();
        Level3.onClick.RemoveAllListeners();
        LoadLevel(2);
    }

    private void OnBackButtonClicked()
    {
        Level1.onClick.RemoveAllListeners();
        Level2.onClick.RemoveAllListeners();
        Level3.onClick.RemoveAllListeners();
        backButton.onClick.RemoveAllListeners();
        playButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        Level1.gameObject.SetActive(false);
        Level2.gameObject.SetActive(false);
        Level3.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
    }

    private void LoadLevel(int level)
    {
        PlayerPrefs.SetInt("CurrentLevel", level);
        SceneManager.LoadScene("Level");
    }

    private void OnQuitButtonClicked()
    {
        // TODO: Quit game through GameManager
        Application.Quit();
    }
}

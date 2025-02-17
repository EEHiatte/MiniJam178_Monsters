using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class that controls the main menu.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    // TODO: Store main menu buttons and other related objects and behavior.
    [SerializeField] private GameObject mainMenuButtonsGroup;
    
    [SerializeField] private Button playButton;
    
    [SerializeField] private Button quitButton;

    [SerializeField] private GameObject levelButtonsGroup;

    [SerializeField] private Button Level1;

    [SerializeField] private Button Level2;

    [SerializeField] private Button Level3;

    [SerializeField] private Button backButton;

    [SerializeField] private Button helpButton;

    private void Start()
    {
        RegisterMenuButtons();
    }

    private void Update()
    {
        //TODO REMOVE CHEATS
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerPrefs.SetInt("CompletedLevel", 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerPrefs.SetInt("CompletedLevel", 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerPrefs.SetInt("CompletedLevel", 2);
        }
    }

    private void RegisterMenuButtons()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        helpButton.onClick.AddListener(OnHelpButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }
    
    private void UnregisterMenuButtons()
    {
        playButton.onClick.RemoveListener(OnPlayButtonClicked);
        helpButton.onClick.RemoveListener(OnHelpButtonClicked);
        quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }

    private void OnHelpButtonClicked()
    {
        UnregisterMenuButtons();
        mainMenuButtonsGroup.SetActive(false);
        levelButtonsGroup.SetActive(false);
    }
    
    private void OnPlayButtonClicked()
    {
        UnregisterMenuButtons();
        mainMenuButtonsGroup.SetActive(false);
        levelButtonsGroup.SetActive(true);
        backButton.onClick.AddListener(OnBackButtonClicked);

        // TODO: Load the level scene
        if (PlayerPrefs.HasKey("CompletedLevel")) 
        {
            switch (PlayerPrefs.GetInt("CompletedLevel") + 1)
            {
                case 0:
                    Level1.interactable = true;
                    Level2.interactable = false;
                    Level3.interactable = false;
                    break;
                case 1:
                    Level1.interactable = true;
                    Level2.interactable = true;
                    Level3.interactable = false;
                    break;
                case 2:
                    Level1.interactable = true;
                    Level2.interactable = true;
                    Level3.interactable = true;
                    break;
            }
        }
        else
        {
            Level1.interactable = true;
            Level2.interactable = false;
            Level3.interactable = false;
        }
        Level1.onClick.AddListener(OnLevel1ButtonClicked);
        Level2.onClick.AddListener(OnLevel2ButtonClicked);
        Level3.onClick.AddListener(OnLevel3ButtonClicked);
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
        RegisterMenuButtons();
        Level1.onClick.RemoveAllListeners();
        Level2.onClick.RemoveAllListeners();
        Level3.onClick.RemoveAllListeners();
        backButton.onClick.RemoveAllListeners();
        mainMenuButtonsGroup.SetActive(true);
        levelButtonsGroup.SetActive(false);
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

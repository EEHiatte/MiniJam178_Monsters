using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public Button returnToMainMenuButton;

    public Button resumeButton;

    public LevelController levelController;

    void OnEnable()
    {
        levelController.paused = true;
        resumeButton.onClick.AddListener(Resume);
        returnToMainMenuButton.onClick.AddListener(ReturnToMainMenu);
        Time.timeScale = 0f;
    }

    void OnDisable()
    {
        returnToMainMenuButton.onClick.RemoveAllListeners();
        resumeButton.onClick.RemoveAllListeners();
        Time.timeScale = 1f;
    }

    public void ReturnToMainMenu()
    {
        returnToMainMenuButton.onClick.RemoveAllListeners();
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        returnToMainMenuButton.onClick.RemoveAllListeners();
        resumeButton.onClick.RemoveAllListeners();
        levelController.paused = false;
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
}

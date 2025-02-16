using System.Diagnostics.Contracts;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class LevelCompleteMenuController : MonoBehaviour
{
    public LevelController levelController;

    public TextMeshProUGUI GoldSpent;

    public TextMeshProUGUI MonstersKilled;

    public TextMeshProUGUI TowersPurchased;

    public TextMeshProUGUI LevelCompleteText;

    public Button returnToMainMenuButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        GoldSpent.text = levelController.GoldSpent.ToString();
        MonstersKilled.text = levelController.MonstersKilled.ToString();
        TowersPurchased.text = levelController.TowersBuilt.ToString();
        returnToMainMenuButton.onClick.AddListener(ReturnToMainMenu);
        switch (levelController.GameState)
        {
            case 1:
                LevelCompleteText.text = "Infected!";
                break;
            case 2:
                //TODO: Set Player Pref to next level completed
                LevelCompleteText.text = "Level Complete!";
                break;
        }
    }

    public void ReturnToMainMenu()
    {
        returnToMainMenuButton.onClick.RemoveAllListeners();
        //TODO: Load the main menu
    }
}

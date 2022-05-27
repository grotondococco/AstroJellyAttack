using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuManager : MenuManager {
    [SerializeField] Button LoadGameButton = default;

    private void Start()
    {
        ShowMainMenu();
        if (SaveManager.GameDataExist()) LoadGameButton.interactable = true;
        else LoadGameButton.interactable = false;
    }
}
